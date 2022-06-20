import type { QueryClientConfig } from 'react-query'

import { GraphQLClient } from 'graphql-request'

const client = new GraphQLClient(process.env.NEXT_PUBLIC_API_URI)

export const fetcher =
  <TData, TVariables>(query: string, variables?: TVariables) =>
  async (): Promise<TData> =>
    client.request<TData, TVariables>(query, variables)

export const options: QueryClientConfig = {
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
    },
  },
}
