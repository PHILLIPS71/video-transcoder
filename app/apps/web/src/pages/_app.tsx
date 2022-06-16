import type { AppProps } from 'next/app'

import React from 'react'
import { Hydrate, QueryClient, QueryClientProvider } from 'react-query'

const Application = ({ Component, pageProps }: AppProps) => {
  const [client] = React.useState(() => new QueryClient())

  return (
    <QueryClientProvider client={client}>
      <Hydrate state={pageProps.dehydratedState}>
        <Component {...pageProps} />
      </Hydrate>
    </QueryClientProvider>
  )
}

export default Application
