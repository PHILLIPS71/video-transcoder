using Bogus;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries;
using Giantnodes.Dashboard.Application.Consumers.FileExplorer.Queries;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Giantnodes.Dashboard.Application.Tests.Consumers.FileExplorer
{
    public class GetDirectoryContentsUnitTests
    {
        private readonly ServiceProvider _provider;
        private readonly MockFileSystem _system = new MockFileSystem(new Dictionary<string, MockFileData> {
            { @"/media/tvshows/Silicon Valley/Season 1", new MockDirectoryData() },
            { @"/media/tvshows/Silicon Valley/Season 1/.DS_Store", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/poster.png", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E01 - Minimum Viable Product.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E02 - The Cap Table.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E03 - Articles of Incorporation.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E04 - Fiduciary Duties.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E05 - Signaling Risk.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E06 - Third Party Insourcing.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E07 - Proof of Concept.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 1/Silicon Valley - S01E08 - Optimal Tip-to-Tip Efficiency.mp4", new MockFileData(string.Empty) },

            { @"/media/tvshows/Silicon Valley/Season 2", new MockDirectoryData() },
            { @"/media/tvshows/Silicon Valley/Season 2/.DS_Store", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/poster.png", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E01 - Sand Hill Shuffle.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E02 - Runaway Devaluation.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E03 - Bad Money.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E04 - The Lady.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E05 - Server Space.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E06 - Homicide.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E07 - Adult Content.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E08 - White Hat/Black Hat.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E09 - Binding Arbitration.mp4", new MockFileData(string.Empty) },
            { @"/media/tvshows/Silicon Valley/Season 2/Silicon Valley - S02E10 - Two Days of the Condor.mp4", new MockFileData(string.Empty) }
        });

        public GetDirectoryContentsUnitTests()
        {
            _provider = new ServiceCollection()
                .AddSingleton<IFileSystem>(_system)
                .AddApplicationTestServices()
                .AddMassTransitTestHarness(options =>
                {
                    options.AddConsumer<GetDirectoryContentsConsumer>();

                })
                .BuildServiceProvider(true);
        }

        [Fact]
        public async Task Reject_With_Invalid_Path()
        {
            // Arrange
            var query = new GetDirectoryContents
            {
                Directory = new Faker().System.FilePath()
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContents>();
            var response = await client.GetResponse<GetDirectoryContentsRejected>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContentsRejected>());
            Assert.Equal(GetDirectoryContentsRejection.DIRECTORY_NOT_FOUND, response.Message.ErrorCode);
        }

        [Fact]
        public async Task Respond_Only_Video_Files()
        {
            // Arrange
            var query = new GetDirectoryContents
            {
                Directory = @"/media/tvshows/Silicon Valley/Season 1"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContents>();
            var response = await client.GetResponse<GetDirectoryContentsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContentsResult>());

            var contents = response.Message;
            Assert.Equal(8, contents.Files.Length);
            Assert.Empty(contents.Directories);
        }

        [Fact]
        public async Task Respond_Directory_Files()
        {
            // Arrange
            var query = new GetDirectoryContents
            {
                Directory = @"/media/tvshows/Silicon Valley/Season 1"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContents>();
            var response = await client.GetResponse<GetDirectoryContentsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContentsResult>());

            var contents = response.Message;
            Assert.Equal(8, contents.Files.Length);
            Assert.Empty(contents.Directories);

            var files = _system.DirectoryInfo.FromDirectoryName(query.Directory).GetFiles();
            foreach (var file in contents.Files.ToList())
            {
                var found = files.FirstOrDefault(f => f.FullName == file.Path);
                Assert.NotNull(found);
                Assert.Equal(file.Name, found?.Name);
                Assert.Equal(file.DirectoryName, found?.DirectoryName);
            }
        }

        [Fact]
        public async Task Respond_Directory_Directories()
        {
            // Arrange
            var query = new GetDirectoryContents
            {
                Directory = @"/media/tvshows/Silicon Valley"
            };

            var harness = _provider.GetRequiredService<ITestHarness>();
            await harness.Start();

            // Act
            var client = harness.GetRequestClient<GetDirectoryContents>();
            var response = await client.GetResponse<GetDirectoryContentsResult>(query);

            // Assert
            Assert.True(await harness.Sent.Any<GetDirectoryContentsResult>());

            var contents = response.Message;
            Assert.Empty(contents.Files);
            Assert.Equal(2, contents.Directories.Length);

            var directories = _system.DirectoryInfo.FromDirectoryName(query.Directory).GetDirectories();
            foreach (var directory in contents.Directories.ToList())
            {
                var found = directories.FirstOrDefault(d => d.FullName == directory.Path);
                Assert.NotNull(found);
                Assert.Equal(directory.Name, found?.Name);
            }
        }
    }
}
