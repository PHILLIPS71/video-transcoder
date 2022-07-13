using Bogus;
using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using Giantnodes.Dashboard.Application.Consumers.Analytics.Queries;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Giantnodes.Dashboard.Application.Tests.Consumers.Analytics.Queries
{
    public class GetDirectoryAnalyticsUnitTests
    {
        private readonly ServiceProvider _provider;
        private readonly MockFileSystem _system = new MockFileSystem(new Dictionary<string, MockFileData> {
            { @"/media/tvshows/Silicon Valley", new MockDirectoryData() },
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
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E08 - Optimal Tip-to-Tip Efficiency.mov", new MockFileData(string.Empty) }
        });

        public GetDirectoryAnalyticsUnitTests()
        {
            _provider = new ServiceCollection()
                .AddSingleton<IFileSystem>(_system)
                .AddApplicationTestServices()
                .AddMassTransitTestHarness(options =>
                {
                    options.AddConsumer<GetDirectoryAnalyticsConsumer>();

                })
                .BuildServiceProvider(true);
        }

        [Fact]
        public async Task Reject_With_Invalid_Path()
        {
            // Arrange
            var query = new GetDirectoryAnalytics
            {
                Directory = new Faker().System.FilePath()
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryAnalytics>();
            var response = await client.GetResponse<GetDirectoryAnalyticsRejected>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryAnalyticsRejected>());
            Assert.Equal(GetDirectoryAnalyticsRejection.DIRECTORY_NOT_FOUND, response.Message.ErrorCode);
        }

        [Fact]
        public async Task Respond_Latest_Modified_File()
        {
            // Arrange
            var path = @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E04 - Fiduciary Duties.mkv";
            var file = _system.GetFile(path);
            file.LastWriteTime = DateTimeOffset.UtcNow;

            var query = new GetDirectoryAnalytics
            {
                Directory = @"/media/tvshows/Silicon Valley"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryAnalytics>();
            var response = await client.GetResponse<GetDirectoryAnalyticsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryAnalyticsResult>());
            Assert.Equal(_system.FileInfo.FromFileName(path).FullName, response.Message.LatestModifiedFile?.Path);
        }

        [Fact]
        public async Task Respond_Total_Media_Files()
        {
            // Arrange
            var query = new GetDirectoryAnalytics
            {
                Directory = @"/media/tvshows/Silicon Valley"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryAnalytics>();
            var response = await client.GetResponse<GetDirectoryAnalyticsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryAnalyticsResult>());
            Assert.Equal(8, response.Message.TotalFiles);
        }
    }
}
