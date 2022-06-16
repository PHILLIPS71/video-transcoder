import gql from 'graphql-tag'
import React from 'react'

import { SortEnumType, useGetDirectoryContentsQuery } from '@/__generated__/graphql-types'

const GET_DIRECTORY_CONTENTS = gql`
  query GetDirectoryContents($input: GetDirectoryContentsQueryInput!, $order: [IFileSystemNodeSortInput!]) {
    directory_contents(input: $input, order: $order) {
      ... on FileSystemFile {
        path
        name
        length
        directory_name
        is_read_only
        created_at_utc
        modified_at_utc
      }
      ... on FileSystemDirectory {
        path
        name
        length
        created_at_utc
        modified_at_utc
      }
    }
  }
`

const Web = () => {
  const { data, error, isFetching } = useGetDirectoryContentsQuery({
    input: {
      directory: '//192.168.1.200/mnt/media/media/tvshows/The Boys/Season 1',
    },
    order: {
      length: SortEnumType.Desc,
    },
  })

  if (isFetching) return <p>Loading...</p>

  if (error) return <p>{error as string}</p>

  return (
    <div>
      <h1>Web</h1>

      <div>
        {data?.directory_contents.map((node) => (
          <p>{node.name}</p>
        ))}
      </div>
    </div>
  )
}

export default Web
