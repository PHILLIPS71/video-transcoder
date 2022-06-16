import type { AppProps } from 'next/app'

import React from 'react'
import { Hydrate, QueryClient, QueryClientProvider } from 'react-query'
import { ThemeProvider } from 'styled-components'

import { LightTheme } from '@giantnodes/ui'

const Application = ({ Component, pageProps }: AppProps) => {
  const [client] = React.useState(() => new QueryClient())

  return (
    <QueryClientProvider client={client}>
      <Hydrate state={pageProps.dehydratedState}>
        <ThemeProvider theme={LightTheme}>
          <Component {...pageProps} />
        </ThemeProvider>
      </Hydrate>
    </QueryClientProvider>
  )
}

export default Application
