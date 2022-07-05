import type { GetDirectoryContentsQuery, GetDirectoryContentsQueryVariables } from '@/__generated__/graphql-types'
import type { GetServerSideProps, NextPage } from 'next'

import { gql } from 'graphql-tag'
import React from 'react'

import { SortEnumType } from '@/__generated__/graphql-types'
import ExploreTable from '@/features/explore/ExploreTable'
import { client } from '@/library/graphql-fetch'

const GET_DIRECTORY_CONTENTS = gql`
  query GetDirectoryContents($input: GetDirectoryContentsQueryInput!, $order: [IFileSystemNodeSortInput!]) {
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

type ExplorePageProps = {
  path: string
}

const ExplorePage: NextPage<ExplorePageProps> = ({ path }: ExplorePageProps) => {
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

  return <ExploreTable directory={directory} />
}

export const getServerSideProps: GetServerSideProps<ExplorePageProps> = async (context) => {
  const params = context.query.slug ? [...context.query.slug] : []
  const path = params.reduce((x, param) => `${x}${param}/`, '')

  return {
    props: {
      path,
    },
  }
}

export default ExplorePage
