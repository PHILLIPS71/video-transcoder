import { GraphQLClient } from 'graphql-request'

const client = new GraphQLClient(process.env.NEXT_PUBLIC_API_URI)

// eslint-disable-next-line import/prefer-default-export
export const fetcher =
  <TData, TVariables>(query: string, variables?: TVariables) =>
  async (): Promise<TData> =>
    client.request<TData, TVariables>(query, variables)
