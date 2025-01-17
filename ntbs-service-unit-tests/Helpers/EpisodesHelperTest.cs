﻿using System;
using System.Collections.Generic;
using System.Linq;
using ntbs_service.Helpers;
using ntbs_service.Models;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Enums;
using ntbs_service.Models.ReferenceEntities;
using Xunit;

namespace ntbs_service_unit_tests.Helpers
{
    public class EpisodeHelperTest
    {
        // Arrange
        private readonly List<TreatmentEvent> _testTreatmentEvents = new List<TreatmentEvent>
        {
            // Episode 1
            new TreatmentEvent
            {
                EventDate = new DateTime(2011, 1, 1), TreatmentEventType = TreatmentEventType.TreatmentStart
            },
            new TreatmentEvent
            {
                EventDate = new DateTime(2011, 6, 1),
                TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                TreatmentOutcome =
                    new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.TreatmentStopped}
            },
            // Transfer
            new TreatmentEvent
            {
                EventDate = new DateTime(2012, 1, 1), TreatmentEventType = TreatmentEventType.TransferOut
            },
            // Episode 2
            new TreatmentEvent
            {
                EventDate = new DateTime(2014, 1, 1),
                TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                TreatmentOutcome = new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.Completed}
            },
            // Episode 3
            new TreatmentEvent
            {
                EventDate = new DateTime(2016, 1, 1),
                TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                TreatmentOutcome = new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.Cured}
            },
            new TreatmentEvent
            {
                EventDate = new DateTime(2015, 1, 1), TreatmentEventType = TreatmentEventType.TreatmentRestart
            },
            // Episode 4
            new TreatmentEvent
            {
                EventDate = new DateTime(2017, 1, 1), TreatmentEventType = TreatmentEventType.TransferIn
            },
            new TreatmentEvent
            {
                EventDate = new DateTime(2020, 1, 1),
                TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                TreatmentOutcome = new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.Died}
            },
            new TreatmentEvent
            {
                EventDate = new DateTime(2019, 1, 1), TreatmentEventType = TreatmentEventType.TreatmentRestart
            },
        };

        [Fact]
        public void GroupEpisodesIntoPeriods_GroupsBasedOnEndingOutcomeType()
        {
            // Act
            var periods = _testTreatmentEvents.GroupEpisodesIntoPeriods();

            // Assert
            Assert.Equal(5, periods.Count);
            var expectedPeriod0 = TreatmentPeriod.CreatePeriodFromEvents(1,
                new List<TreatmentEvent> {_testTreatmentEvents[0], _testTreatmentEvents[1]},
                _testTreatmentEvents[1].EventDate);

            var expectedPeriod1 = TreatmentPeriod.CreateTransferPeriod(_testTreatmentEvents[2]);
            
            var expectedPeriod2 = TreatmentPeriod.CreateTreatmentPeriod(2, _testTreatmentEvents[3]);

            var expectedPeriod3 = TreatmentPeriod.CreatePeriodFromEvents(3,
                new List<TreatmentEvent> {_testTreatmentEvents[5], _testTreatmentEvents[4] },
                _testTreatmentEvents[4].EventDate);

            var expectedPeriod4 = TreatmentPeriod.CreatePeriodFromEvents(4,
                new List<TreatmentEvent> { _testTreatmentEvents[6], _testTreatmentEvents[8], _testTreatmentEvents[7]},
                _testTreatmentEvents[7].EventDate);

            AssertTreatmentPeriodMatchesExpected(expectedPeriod0, periods[0]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod1, periods[1]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod2, periods[2]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod3, periods[3]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod4, periods[4]);
        }

        [Fact]
        public void GroupEpisodesIntoPeriods_GroupsTransfers()
        {
            // Arrange
            List<TreatmentEvent> testTransferEvents = new List<TreatmentEvent>
            {
                // Episode 1
                new TreatmentEvent
                {
                    EventDate = new DateTime(2010, 1, 1), TreatmentEventType = TreatmentEventType.TreatmentStart
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2011, 1, 1),
                    TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                    TreatmentOutcome =
                        new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.TreatmentStopped}
                },
                // Transfer
                new TreatmentEvent
                {
                    EventDate = new DateTime(2012, 1, 1), TreatmentEventType = TreatmentEventType.TransferOut
                },
                // Episode 2
                new TreatmentEvent
                {
                    EventDate = new DateTime(2013, 1, 1), TreatmentEventType = TreatmentEventType.TransferIn
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2014, 1, 1), TreatmentEventType = TreatmentEventType.TreatmentRestart
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2015, 1, 1), TreatmentEventType = TreatmentEventType.TransferOut
                },
                // Episode 3
                new TreatmentEvent
                {
                    EventDate = new DateTime(2016, 1, 1), TreatmentEventType = TreatmentEventType.TransferIn
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2017, 1, 1),
                    TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                    TreatmentOutcome = new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.Completed}
                },
            };

            // Act
            var periods = testTransferEvents.GroupEpisodesIntoPeriods();

            // Assert
            Assert.Equal(4, periods.Count);
            var expectedPeriod0 = TreatmentPeriod.CreatePeriodFromEvents(1,
                new List<TreatmentEvent> { testTransferEvents[0], testTransferEvents[1] }, 
                testTransferEvents[1].EventDate);

            var expectedPeriod1 = TreatmentPeriod.CreateTransferPeriod(testTransferEvents[2]);

            var expectedPeriod2 = TreatmentPeriod.CreatePeriodFromEvents(2,
                new List<TreatmentEvent> { testTransferEvents[3], testTransferEvents[4], testTransferEvents[5] }, 
                testTransferEvents[5].EventDate);

            var expectedPeriod3 = TreatmentPeriod.CreatePeriodFromEvents(3,
                new List<TreatmentEvent> { testTransferEvents[6], testTransferEvents[7] },
                testTransferEvents[7].EventDate);

            AssertTreatmentPeriodMatchesExpected(expectedPeriod0, periods[0]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod1, periods[1]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod2, periods[2]);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod3, periods[3]);
        }

        [Fact]
        public void GetMostRecentTreatmentEvent_ReturnsLastTreatmentEvent()
        {
            // Act
            var treatmentEvent = _testTreatmentEvents.GetMostRecentTreatmentEvent();

            // Assert
            Assert.Equal(treatmentEvent.EventDate, new DateTime(2020, 1, 1));
        }

        [Fact]
        public void GroupEpisodesIntoPeriods_CorrectlySortsThroughEventsOnTheSameDay()
        {
            // Arrange
            var treatmentEvents = new List<TreatmentEvent>
            {
                new TreatmentEvent
                {
                    EventDate = new DateTime(2020, 04, 16), TreatmentEventType = TreatmentEventType.TreatmentStart,
                },
                new TreatmentEvent
                {
                    // The treatment events tend to be with time and not just date!
                    EventDate = new DateTime(2020, 07, 1, 10, 43, 10),
                    TreatmentEventType = TreatmentEventType.TransferOut,
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2020, 07, 1, 10, 43, 10),
                    TreatmentEventType = TreatmentEventType.TransferIn,
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2020, 07, 01),
                    TreatmentEventType = TreatmentEventType.TreatmentRestart,
                },
                new TreatmentEvent
                {
                    EventDate = new DateTime(2020, 07, 03),
                    TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                    TreatmentOutcome = new TreatmentOutcome
                    {
                        TreatmentOutcomeType = TreatmentOutcomeType.Completed,
                        TreatmentOutcomeSubType = TreatmentOutcomeSubType.Other
                    }
                }
            };

            // Act
            var periods = treatmentEvents.GroupEpisodesIntoPeriods();

            // Assert
            Assert.Collection(periods,
                period => Assert.Collection(period.TreatmentEvents,
                    ev => Assert.Equal(TreatmentEventType.TreatmentStart, ev.TreatmentEventType),
                    ev => Assert.Equal(TreatmentEventType.TransferOut, ev.TreatmentEventType)
                ),
                period => Assert.Collection(period.TreatmentEvents,
                    ev => Assert.Equal(TreatmentEventType.TransferIn, ev.TreatmentEventType),
                    ev => Assert.Equal(TreatmentEventType.TreatmentRestart, ev.TreatmentEventType),
                    ev => Assert.Equal(TreatmentEventType.TreatmentOutcome, ev.TreatmentEventType)
                )
            );
        }

        [Fact]
        public void GroupEpisodesIntoPeriods_CorrectlyCreatesOnePeriodWithDeathDateForPostMortemCase()
        {
            // Arrange
            var dateOfDeath = new DateTime(1999, 12, 12);
            var treatmentEvents = new List<TreatmentEvent>
            {
                new TreatmentEvent
                {
                    TreatmentEventType = TreatmentEventType.TreatmentOutcome,
                    TreatmentOutcome = new TreatmentOutcome {TreatmentOutcomeType = TreatmentOutcomeType.Died},
                    EventDate = dateOfDeath
                },
                new TreatmentEvent
                {
                    TreatmentEventType = TreatmentEventType.DiagnosisMade, EventDate = dateOfDeath.AddDays(15)
                }
            };
            var expectedPeriod = TreatmentPeriod.CreatePeriodFromEvents(1, treatmentEvents, dateOfDeath);

            // Act
            var periods = treatmentEvents.GroupEpisodesIntoPeriods(isPostMortemWithCorrectEvents: true);

            // Assert
            Assert.Single(periods);
            AssertTreatmentPeriodMatchesExpected(expectedPeriod, periods.Single());
        }

        [Theory]
        [InlineData(TreatmentEventType.Denotification, null, null, true)]
        [InlineData(TreatmentEventType.TransferOut, null, null, true)]
        [InlineData(TreatmentEventType.TransferIn, null, null, false)]
        [InlineData(TreatmentEventType.TreatmentRestart, null, null, false)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.Completed, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.Cured, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.Died, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.Lost, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.Failed, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.TreatmentStopped, null, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.NotEvaluated, TreatmentOutcomeSubType.StillOnTreatment, false)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.NotEvaluated, TreatmentOutcomeSubType.Other, true)]
        [InlineData(TreatmentEventType.TreatmentOutcome, TreatmentOutcomeType.NotEvaluated, TreatmentOutcomeSubType.TransferredAbroad, true)]
        public void IsEpisodeEndingTreatmentEvent_CorrectlyIdentifiesEndingEvents
            (TreatmentEventType eventType, TreatmentOutcomeType? outcomeType, TreatmentOutcomeSubType? subType, bool expectedEnding)
        {
            // Arrange
            var possibleEndingEvent = new TreatmentEvent
            {
                TreatmentEventType = eventType,
                TreatmentOutcome = outcomeType.HasValue
                    ? new TreatmentOutcome {
                        TreatmentOutcomeType = outcomeType.Value,
                        TreatmentOutcomeSubType = subType }
                    : null
            };

            // Act
            var isEnding = possibleEndingEvent.IsEpisodeEndingTreatmentEvent();

            // Assert
            Assert.Equal(expectedEnding, isEnding);
        }

        private void AssertTreatmentPeriodMatchesExpected(TreatmentPeriod expected, TreatmentPeriod actual)
        {
            Assert.Equal(expected.TreatmentEvents, actual.TreatmentEvents);
            Assert.Equal(expected.PeriodNumber, actual.PeriodNumber);
            Assert.Equal(expected.IsTransfer, actual.IsTransfer);
            Assert.Equal(expected.PeriodStartDate, actual.PeriodStartDate);
            Assert.Equal(expected.PeriodEndDate, actual.PeriodEndDate);
        }
    }
}
