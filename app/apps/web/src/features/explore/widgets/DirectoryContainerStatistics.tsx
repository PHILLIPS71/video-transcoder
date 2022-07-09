import gql from 'graphql-tag'
import React from 'react'
import styled from 'styled-components'

import { Block } from '@giantnodes/ui'

import { SortEnumType, useGetDirectoryContainerStatisticsQuery } from '@/__generated__/graphql-types'
import FileTypeColours from '@/layouts/constants/file-types'

type DirectoryContainerStatisticsProps = {
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

const GET_DIRECTORY_CONTAINER_STATISTICS = gql`
  query GetDirectoryContainerStatistics(
    $input: GetDirectoryContainerStatisticsQueryInput!
    $order: [DirectoryContainerStatisticSortInput!]
  ) {
    directory_container_statistics(input: $input, order: $order) {
      extension
      percent
    }
  }
`

const DirectoryContainerStatistics: React.FC<DirectoryContainerStatisticsProps> = ({ path }) => {
  const { data } = useGetDirectoryContainerStatisticsQuery({
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
        {data?.directory_container_statistics.map((statistic) => (
          <TrackBar extension={statistic.extension} percent={statistic.percent} />
        ))}
      </Track>

      <Legend>
        {data?.directory_container_statistics.map((statistic) => (
          <LegendItem>
            <Block display="inline-flex" mr="16px" gridGap="8px">
              <svg
                fill={FileTypeColours[statistic.extension]}
                aria-hidden="true"
                height="16"
                viewBox="0 0 16 16"
                version="1.1"
                width="16"
                data-view-component="true"
              >
                <path fillRule="evenodd" d="M8 4a4 4 0 100 8 4 4 0 000-8z" />
              </svg>

              <Block fontWeight={700}>{statistic.extension}</Block>
              <Block color="text.secondary">{statistic.percent}%</Block>
            </Block>
          </LegendItem>
        ))}
      </Legend>
    </>
  )
}

export default DirectoryContainerStatistics
