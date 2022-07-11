using Bogus;
using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using Giantnodes.Dashboard.Application.Consumers.Analytics.Queries;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Giantnodes.Dashboard.Application.Tests.Consumers.Analytics.Queries
{
    public class GetDirectoryContainerAnalyticsUnitTests
    {
        private readonly ServiceProvider _provider;
        private readonly MockFileSystem _system = new MockFileSystem(new Dictionary<string, MockFileData> {
            { @"/media/tvshows/Silicon Valley/Season 1", new MockDirectoryData() },
            { @"/media/tvshows/Silicon Valley/Season 1/.DS_Store", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/poster.png", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E01 - Minimum Viable Product.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E02 - The Cap Table.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E03 - Articles of Incorporation.mkv", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E04 - Fiduciary Duties.mkv", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E05 - Signaling Risk.avi", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E06 - Third Party Insourcing.avi", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E07 - Proof of Concept.mov", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E08 - Optimal Tip-to-Tip Efficiency.mov", new MockFileData(string.Empty) },
        });

        public GetDirectoryContainerAnalyticsUnitTests()
        {
            _provider = new ServiceCollection()
                .AddSingleton<IFileSystem>(_system)
                .AddApplicationTestServices()
                .AddMassTransitTestHarness(options =>
                {
                    options.AddConsumer<GetDirectoryContainerAnalyticsConsumer>();

                })
                .BuildServiceProvider(true);
        }

        [Fact]
        public async Task Reject_With_Invalid_Path()
        {
            // Arrange
            var query = new GetDirectoryContainerAnalytics
            {
                Directory = new Faker().System.FilePath()
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContainerAnalytics>();
            var response = await client.GetResponse<GetDirectoryContainerAnalyticsRejected>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContainerAnalyticsRejected>());
            Assert.Equal(GetDirectoryContainerAnalyticsRejection.DIRECTORY_NOT_FOUND, response.Message.ErrorCode);
        }

        [Fact]
        public async Task Respond_Directory_Container_Distribution()
        {
            // Arrange
            var query = new GetDirectoryContainerAnalytics
            {
                Directory = @"/media/tvshows/Silicon Valley/Season 1"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContainerAnalytics>();
            var response = await client.GetResponse<GetDirectoryContainerAnalyticsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContainerAnalyticsResult>());

            var containers = response.Message.Containers.ToList();
            Assert.Equal(4, containers.Count);

            Assert.Equal(25, containers.First(c => c.Extension == ".mp4").Percent);
            Assert.Equal(25, containers.First(c => c.Extension == ".mkv").Percent);
            Assert.Equal(25, containers.First(c => c.Extension == ".avi").Percent);
            Assert.Equal(25, containers.First(c => c.Extension == ".mov").Percent);
            Assert.Equal(100, containers.Sum(c => c.Percent));
        }
    }
}
