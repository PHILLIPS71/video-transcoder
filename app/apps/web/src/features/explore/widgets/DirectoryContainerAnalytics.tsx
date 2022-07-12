import type {
  DirectoryContainerAnalytics as DirectoryContainerAnalyticsResult,
  GetDirectoryContainerAnalyticsQuery,
  GetDirectoryContainerAnalyticsQueryVariables,
} from '@/__generated__/graphql-types'
import type { GetServerSideProps } from 'next'

import gql from 'graphql-tag'
import React from 'react'
import styled from 'styled-components'

import { Block } from '@giantnodes/ui'

import { SortEnumType } from '@/__generated__/graphql-types'
import FileTypeColours from '@/layouts/constants/file-types'
import { client } from '@/library/graphql-fetch'

type DirectoryContainerAnalyticsProps = {
  analytics: DirectoryContainerAnalyticsResult[]
}

type DirectoryContainerAnalyticsComponent = React.FC<DirectoryContainerAnalyticsProps> & {
  getServerSideProps: GetServerSideProps<DirectoryContainerAnalyticsProps>
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

const DirectoryContainerAnalytics: DirectoryContainerAnalyticsComponent = ({ analytics }) => (
  <>
    <Track>
      {analytics?.map((container) => (
        <TrackBar extension={container.extension} percent={container.percent} />
      ))}
    </Track>

    <Legend>
      {analytics?.map((container) => (
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

DirectoryContainerAnalytics.getServerSideProps = async (context) => {
  const params = context.query.slug ? [...context.query.slug] : []
  const path = params.reduce((x, param) => `${x}${param}/`, '')

  const data = await client.request<GetDirectoryContainerAnalyticsQuery, GetDirectoryContainerAnalyticsQueryVariables>(
    GET_DIRECTORY_CONTAINER_ANALYTICS,
    {
      input: {
        directory: `${process.env.NEXT_PUBLIC_LIBRARY_DIRECTORY}/${path}`,
      },
      order: {
        percent: SortEnumType.Desc,
      },
    }
  )

  return {
    props: {
      analytics: data.directory_container_analytics,
    },
  }
}

export default DirectoryContainerAnalytics
