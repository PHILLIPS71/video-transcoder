import gql from 'graphql-tag'
import React from 'react'
import styled from 'styled-components'

import { Block } from '@giantnodes/ui'

import { SortEnumType, useGetDirectoryContainerAnalyticsQuery } from '@/__generated__/graphql-types'
import FileTypeColours from '@/layouts/constants/file-types'

type DirectoryContainerAnalyticsProps = {
  path: string
}

type TrackBarProps = {
  extension: string
  percent: number
}

const Track = styled.span`
  display: flex;
  height: 8px;
  background-color: rgba(0, 0, 0, 0.2);
  border-radius: 6px;
  overflow: hidden;
`

const TrackBar = styled.span<TrackBarProps>`
  width: ${(props) => `${props.percent}%`};
  background-color: ${(props) => FileTypeColours[props.extension]};

  + span {
    margin-left: 2px;
  }
`

const Legend = styled.ul`
  list-style: none;
  font-size: 12px;
  margin: 10px 0 0 0;
  padding: 0px;
`

const LegendItem = styled.li`
  display: inline;
`

const GET_DIRECTORY_CONTAINER_ANALYTICS = gql`
  query GetDirectoryContainerAnalytics(
    $input: GetDirectoryContainerAnalyticsInput!
    $order: [DirectoryContainerAnalyticsSortInput!]
  ) {
    directory_container_analytics(input: $input, order: $order) {
      extension
      percent
    }
  }
`

const DirectoryContainerAnalytics: React.FC<DirectoryContainerAnalyticsProps> = ({ path }) => {
  const { data } = useGetDirectoryContainerAnalyticsQuery({
    input: {
      directory: `${process.env.NEXT_PUBLIC_LIBRARY_DIRECTORY}/${path}`,
    },
    order: {
      percent: SortEnumType.Desc,
    },
  })

  return (
    <>
      <Track>
        {data?.directory_container_analytics.map((container) => (
          <TrackBar extension={container.extension} percent={container.percent} />
        ))}
      </Track>

      <Legend>
        {data?.directory_container_analytics.map((container) => (
          <LegendItem>
            <Block display="inline-flex" mr="16px" gridGap="8px">
              <svg
                fill={FileTypeColours[container.extension]}
                aria-hidden="true"
                height="16"
                viewBox="0 0 16 16"
                version="1.1"
                width="16"
                data-view-component="true"
              >
                <path fillRule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8z" />
              </svg>

              <Block fontWeight={700}>{container.extension}</Block>
              <Block color="text.secondary">{container.percent}%</Block>
            </Block>
          </LegendItem>
        ))}
      </Legend>
    </>
  )
}

export default DirectoryContainerAnalytics
