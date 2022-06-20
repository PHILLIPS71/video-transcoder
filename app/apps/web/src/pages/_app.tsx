import type { AppProps } from 'next/app'

import React from 'react'
import { Hydrate, QueryClient, QueryClientProvider } from 'react-query'
import { ThemeProvider, createGlobalStyle } from 'styled-components'

import { DarkTheme } from '@giantnodes/ui'

import DefaultLayout from '@/layouts/DefaultLayout'
import { options } from '@/library/graphql-request'

import 'public/scss/main.scss'

const GlobalStyle = createGlobalStyle`
  html, body {
    font-family: ${(props) => props.theme.fonts.text};
    background-color:  ${(props) => props.theme.colours.background.primary};
    color:  ${(props) => props.theme.colours.text.primary};
    position: relative;
    overflow-y: hidden;
    height: 100%;
    width: 100%;
    margin: 0px;
    padding: 0px;
  }
  
  #__next {
    height: 100%;
    width: 100%;
    position: relative;
    overflow-x: hidden;
  }

  ::-webkit-scrollbar {
    width: 16px;
  }

  ::-webkit-scrollbar-track {
    box-shadow: inset 0 0 10px 10px transparent;
    background: transparent;
  }

  ::-webkit-scrollbar-thumb {
    box-shadow: inset 0 0 10px 10px ${(props) => props.theme.colours.background.tertiary};
    border: solid 5px transparent;
    border-radius: 16px;
  }
`

const Application = ({ Component, pageProps }: AppProps) => {
  const [client] = React.useState(() => new QueryClient(options))

  return (
    <QueryClientProvider client={client}>
      <Hydrate state={pageProps.dehydratedState}>
        <ThemeProvider theme={DarkTheme}>
          <GlobalStyle />

          <DefaultLayout>
            <Component {...pageProps} />
          </DefaultLayout>
        </ThemeProvider>
      </Hydrate>
    </QueryClientProvider>
  )
}

export default Application
