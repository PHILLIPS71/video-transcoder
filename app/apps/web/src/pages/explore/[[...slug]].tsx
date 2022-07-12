import type {
  DirectoryContainerAnalytics as DirectoryContainerAnalyticsResult,
  GetDirectoryAnalyticsResult,
  GetDirectoryContentsQuery,
  GetDirectoryContentsQueryVariables,
} from '@/__generated__/graphql-types'
import type { GetServerSideProps, NextPage } from 'next'

import { Trans } from '@lingui/macro'
import { gql } from 'graphql-tag'
import React from 'react'

import { Card, Grid, Heading } from '@giantnodes/ui'

import { SortEnumType } from '@/__generated__/graphql-types'
import ExploreTable from '@/features/explore/ExploreTable'
import DirectoryAnalytics from '@/features/explore/widgets/DirectoryAnalytics'
import DirectoryContainerAnalytics from '@/features/explore/widgets/DirectoryContainerAnalytics'
import { client } from '@/library/graphql-fetch'

type ExplorePageProps = {
  path: string
  analytics: GetDirectoryAnalyticsResult
  containers: DirectoryContainerAnalyticsResult[]
}

const GET_DIRECTORY_CONTENTS = gql`
  query GetDirectoryContents($input: GetDirectoryContentsInput!, $order: [IFileSystemNodeSortInput!]) {
    directory_contents(input: $input, order: $order) {
      __typename
      ... on FileSystemFile {
        path
        name
        ... @defer {
          length
        }
        directory_name
        is_read_only
        created_at_utc
        modified_at_utc
      }
      ... on FileSystemDirectory {
        path
        name
        ... @defer {
          length
        }
        created_at_utc
        modified_at_utc
      }
    }
  }
`

const ExplorePage: NextPage<ExplorePageProps> = ({ path, analytics, containers }: ExplorePageProps) => {
  const [directory, setDirectory] = React.useState<GetDirectoryContentsQuery['directory_contents']>()

  const getDirectoryContents = React.useCallback(() => {
    client.stream<GetDirectoryContentsQuery, GetDirectoryContentsQueryVariables>(
      GET_DIRECTORY_CONTENTS,
      {
        input: {
          directory: `${process.env.NEXT_PUBLIC_LIBRARY_DIRECTORY}/${path}`,
        },
        order: {
          name: SortEnumType.Asc,
        },
      },
      {
        next: (data) => setDirectory(data?.directory_contents),
        complete: (data) => setDirectory(data?.directory_contents),
      }
    )
  }, [path])

  React.useEffect(() => {
    getDirectoryContents()
  }, [getDirectoryContents])

  if (!directory) {
    return <>LOADING...</>
  }

  return (
    <Grid>
      <Grid.Column span={[4, 8, 8, 12]}>
        <ExploreTable directory={directory} />
      </Grid.Column>

      <Grid.Column span={[4, 8, 4, 4]}>
        <Card>
          <Card.Header>
            <Heading level={6}>
              <Trans>About</Trans>
            </Heading>
          </Card.Header>
          <Card.Body>
            <DirectoryAnalytics analytics={analytics} />
          </Card.Body>
        </Card>

        <Card>
          <Card.Header>
            <Heading level={6}>
              <Trans>File Containers</Trans>
            </Heading>
          </Card.Header>
          <Card.Body>
            <DirectoryContainerAnalytics analytics={containers} />
          </Card.Body>
        </Card>
      </Grid.Column>
    </Grid>
  )
}

export const getServerSideProps: GetServerSideProps<ExplorePageProps> = async (context) => {
  const params = context.query.slug ? [...context.query.slug] : []
  const path = params.reduce((x, param) => `${x}${param}/`, '')

  const promises = [
    DirectoryAnalytics.getServerSideProps(context),
    DirectoryContainerAnalytics.getServerSideProps(context),
  ]

  const components = await Promise.all(promises)

  return {
    props: {
      path,
      analytics: components[0]?.props?.analytics,
      containers: components[1].props?.analytics,
    },
  }
}

export default ExplorePage
