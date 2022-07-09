import type { AppProps } from 'next/app'

import React from 'react'
import { Hydrate, QueryClient, QueryClientProvider } from 'react-query'
import { createGlobalStyle } from 'styled-components'

import { LocaleProvider } from '@/contexts/i18n/LocaleContext'
import { ThemeProvider } from '@/contexts/theme/ThemeContext'
import DefaultLayout from '@/layouts/DefaultLayout'
import { options } from '@/library/graphql-fetch'

import 'public/scss/main.scss'

const GlobalStyle = createGlobalStyle`
  html, body {
    font-family: ${(props) => props.theme.fonts.text};
    background-color:  ${(props) => props.theme.colors.background.secondary};
    color:  ${(props) => props.theme.colors.text.primary};
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
    box-shadow: inset 0 0 10px 10px ${(props) => props.theme.colors.background.tertiary};
    border: solid 5px transparent;
    border-radius: 16px;
  }

  a {
    text-decoration: none;
    color:  ${(props) => props.theme.colors.text.primary};

    &:hover {
      text-decoration: underline;
      color:  ${(props) => props.theme.colors.primary};
    }
  }
`

const Application = ({ Component, pageProps }: AppProps) => {
  const [client] = React.useState(() => new QueryClient(options))

  return (
    <QueryClientProvider client={client}>
      <Hydrate state={pageProps.dehydratedState}>
        <LocaleProvider>
          <ThemeProvider>
            <GlobalStyle />

            <DefaultLayout>
              <Component {...pageProps} />
            </DefaultLayout>
          </ThemeProvider>
        </LocaleProvider>
      </Hydrate>
    </QueryClientProvider>
  )
}

export default Application
