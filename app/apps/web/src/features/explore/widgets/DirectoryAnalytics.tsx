import type {
  GetDirectoryAnalyticsQuery,
  GetDirectoryAnalyticsQueryVariables,
  GetDirectoryAnalyticsResult,
} from '@/__generated__/graphql-types'
import type { GetServerSideProps } from 'next'

import dayjs from 'dayjs'
import gql from 'graphql-tag'
import React from 'react'
import { Clock, Edit3 } from 'react-feather'
import styled from 'styled-components'

import { client } from '@/library/graphql-fetch'

type DirectoryAnalyticsProps = {
  analytics: GetDirectoryAnalyticsResult
}

type DirectoryAnalyticsComponent = React.FC<DirectoryAnalyticsProps> & {
  getServerSideProps: GetServerSideProps<DirectoryAnalyticsProps>
}

const AboutList = styled.dl`
  margin: 0px;
`

const AboutListItem = styled.div`
  display: flex;
  grid-gap: 8px;

  &:not(:last-child) {
    margin-bottom: 8px;
  }
`

const AboutIcon = styled.dt`
  display: inline-flex;

  svg {
    color: ${(props) => props.theme.colors.text.secondary};
  }
`

const AboutDescription = styled.dd`
  display: inline;
  font-size: 14px;
  margin: 0;
`

const GET_DIRECTORY_ANALYTICS = gql`
  query GetDirectoryAnalytics($input: GetDirectoryAnalyticsInput!) {
    directory_analytics(input: $input) {
      watch_time_minutes
      latest_modified_file {
        path
        name
        length
        directory_name
        is_read_only
        created_at_utc
        modified_at_utc
      }
    }
  }
`

const DirectoryAnalytics: DirectoryAnalyticsComponent = ({ analytics }) => (
  <AboutList>
    <AboutListItem>
      <AboutIcon>
        <Clock size={16} />
      </AboutIcon>
      <AboutDescription>12 hours of watch time</AboutDescription>
    </AboutListItem>

    <AboutListItem>
      <AboutIcon title={analytics?.latest_modified_file?.name}>
        <Edit3 size={16} />
      </AboutIcon>
      <AboutDescription title={dayjs(analytics?.latest_modified_file?.modified_at_utc).format('LLLL')}>
        {dayjs(analytics?.latest_modified_file?.modified_at_utc).fromNow()}
      </AboutDescription>
    </AboutListItem>
  </AboutList>
)

DirectoryAnalytics.getServerSideProps = async (context) => {
  const params = context.query.slug ? [...context.query.slug] : []
  const path = params.reduce((x, param) => `${x}${param}/`, '')

  const data = await client.request<GetDirectoryAnalyticsQuery, GetDirectoryAnalyticsQueryVariables>(
    GET_DIRECTORY_ANALYTICS,
    {
      input: {
        directory: `${process.env.NEXT_PUBLIC_LIBRARY_DIRECTORY}/${path}`,
      },
    }
  )

  return {
    props: {
      analytics: data.directory_analytics,
    },
  }
}

export default DirectoryAnalytics
