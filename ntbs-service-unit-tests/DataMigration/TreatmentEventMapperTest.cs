﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using ntbs_service.DataAccess;
using ntbs_service.DataMigration;
using ntbs_service.DataMigration.RawModels;
using ntbs_service.Models.Enums;
using ntbs_service.Models.ReferenceEntities;
using Xunit;

namespace ntbs_service_unit_tests.DataMigration
{
    public class TreatmentEventMapperTest : IDisposable
    {
        private readonly ITreatmentEventMapper _treatmentEventMapper;
        private readonly ICaseManagerImportService _caseManagerImportService;
        private readonly IReferenceDataRepository _referenceDataRepository;
        private readonly NtbsContext _context;
        private Dictionary<string, MigrationLegacyUser> _usernameToLegacyUserDict = new Dictionary<string, MigrationLegacyUser>();
        private Dictionary<string, IEnumerable<MigrationLegacyUserHospital>> _usernameToLegacyUserHospitalDict = new Dictionary<string, IEnumerable<MigrationLegacyUserHospital>>();

        public TreatmentEventMapperTest()
        {
            _context = SetupTestContext();
            _referenceDataRepository = new ReferenceDataRepository(_context);
            var userRepo = new UserRepository(_context);
            var migrationRepo = new Mock<IMigrationRepository>();
            migrationRepo.Setup(mr => mr.GetLegacyUserByUsername(It.IsAny<string>()))
                .Returns((string username) => Task.FromResult(_usernameToLegacyUserDict[username]));
            migrationRepo.Setup(repo => repo.GetLegacyUserHospitalsByUsername(It.IsAny<string>()))
                .ReturnsAsync((string username) => new List<MigrationLegacyUserHospital>());
            var importLogger = new Mock<IImportLogger>();
            _caseManagerImportService =
                new CaseManagerImportService(userRepo, _referenceDataRepository, migrationRepo.Object, importLogger.Object);
            _treatmentEventMapper = new TreatmentEventMapper(_caseManagerImportService, _referenceDataRepository);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task MigrationTreatmentEventMappedCorrectlyWithTbServiceAndCaseManagerAdded()
        {
            // Arrange
            GivenLegacyUserWithName("miles.davis@columbia.nhs.uk", "Miles", "Davis");
            GivenHospitalIdHasTbServiceCode(new Guid("B8AA918D-233F-4C41-B9AE-BE8A8DC8BE7B"), "TBS00JAZZ");
            var migrationTransferEvent = new MigrationDbTransferEvent
            {
                EventDate = DateTime.Parse("12/12/2012"),
                CaseManager = "miles.davis@columbia.nhs.uk",
                HospitalId = new Guid("B8AA918D-233F-4C41-B9AE-BE8A8DC8BE7B"),
                TreatmentEventType = "TransferIn"
            };

            // Act
            var mappedEvent = await _treatmentEventMapper.AsTransferEvent(migrationTransferEvent);

            // Assert
            Assert.Equal(DateTime.Parse("12/12/2012"), mappedEvent.EventDate);
            Assert.Equal(TreatmentEventType.TransferIn, mappedEvent.TreatmentEventType);
            Assert.Equal("TBS00JAZZ", mappedEvent.TbServiceCode);
            Assert.NotNull(_context.User.SingleOrDefault(u => u.Username == "miles.davis@columbia.nhs.uk"));
            Assert.Equal(_context.User.Single().Id, mappedEvent.CaseManagerId);
        }

        [Fact]
        public async Task MigrationOutcomeEventMappedCorrectlyWithTbServiceAndCaseManagerAdded()
        {
            // Arrange
            GivenLegacyUserWithName("milicent.davis@columbia.nhs.uk", "Milicent", "Davis");
            GivenHospitalIdHasTbServiceCode(new Guid("B8AA918D-233F-4C41-B9AE-BE8A8DC8BE7B"), "TBS00JAZZ");
            var migrationTransferEvent = new MigrationDbOutcomeEvent
            {
                EventDate = DateTime.Parse("12/12/2012"),
                CaseManager = "milicent.davis@columbia.nhs.uk",
                NtbsHospitalId = new Guid("B8AA918D-233F-4C41-B9AE-BE8A8DC8BE7B"),
                TreatmentEventType = "TransferIn",
                TreatmentOutcomeId = 2,
                Note = "The patient had a specific outcome"
            };

            // Act
            var mappedEvent = await _treatmentEventMapper.AsOutcomeEvent(migrationTransferEvent);

            // Assert
            Assert.Equal(DateTime.Parse("12/12/2012"), mappedEvent.EventDate);
            Assert.Equal(TreatmentEventType.TransferIn, mappedEvent.TreatmentEventType);
            Assert.Equal("The patient had a specific outcome", mappedEvent.Note);
            Assert.Equal(2, mappedEvent.TreatmentOutcomeId);
            Assert.Equal("TBS00JAZZ", mappedEvent.TbServiceCode);
            Assert.NotNull(_context.User.SingleOrDefault(u => u.Username == "milicent.davis@columbia.nhs.uk"));
            Assert.Equal(_context.User.Single().Id, mappedEvent.CaseManagerId);
        }

        [Fact]
        public async Task MigrationOutcomeEventMappedCorrectlyWithNoTbServiceOrCaseManagerAdded()
        {
            // Arrange
            GivenLegacyUserWithName("milicent.davis@columbia.nhs.uk", "Milicent", "Davis");
            var migrationTransferEvent = new MigrationDbOutcomeEvent
            {
                EventDate = DateTime.Parse("12/12/2012"),
                NtbsHospitalId = new Guid("B8AA918D-233F-4C41-B9AE-BE8A8DC8BE7B"),
                TreatmentEventType = "TransferIn",
                TreatmentOutcomeId = 2,
                Note = "The patient had a specific outcome"
            };

            // Act
            var mappedEvent = await _treatmentEventMapper.AsOutcomeEvent(migrationTransferEvent);

            // Assert
            Assert.Equal(DateTime.Parse("12/12/2012"), mappedEvent.EventDate);
            Assert.Equal(TreatmentEventType.TransferIn, mappedEvent.TreatmentEventType);
            Assert.Equal("The patient had a specific outcome", mappedEvent.Note);
            Assert.Equal(2, mappedEvent.TreatmentOutcomeId);
            Assert.Null(mappedEvent.TbServiceCode);
            Assert.Null(mappedEvent.CaseManagerId);
        }

        private void GivenLegacyUserWithName(string username, string givenName, string familyName)
        {
            _usernameToLegacyUserDict.Add(
                username,
                new MigrationLegacyUser
                {
                    Username = username, GivenName = givenName, FamilyName = familyName
                }
            );
        }

        private void GivenHospitalIdHasTbServiceCode(Guid hospitalId, string tbServiceCode)
        {
            _context.Hospital.Add(new Hospital {TBServiceCode = tbServiceCode, HospitalId = hospitalId});
            _context.TbService.Add(new TBService {Code = tbServiceCode});
            _context.SaveChanges();
        }

        private NtbsContext SetupTestContext()
        {
            // Generating a unique database name makes sure the database is not shared between tests.
            string dbName = Guid.NewGuid().ToString();
            return new NtbsContext(new DbContextOptionsBuilder<NtbsContext>()
                .UseInMemoryDatabase(dbName)
                .Options
            );
        }
    }
}
