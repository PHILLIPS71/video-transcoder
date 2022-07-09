import type { DefaultTheme } from 'styled-components'

import React from 'react'
import { ThemeProvider as StyledComponentsThemeProvider } from 'styled-components'

import { DarkTheme, LightTheme } from '@giantnodes/ui'

export type ThemeContext = {
  theme: DefaultTheme
  setTheme: React.Dispatch<React.SetStateAction<DefaultTheme>>
}

type ThemeProviderProps = {
  children: React.ReactElement | React.ReactElement[]
}

const Context = React.createContext<ThemeContext>({
  theme: LightTheme,
  setTheme: () => null,
})

export const ThemeProvider: React.FC<ThemeProviderProps> = ({ children }) => {
  const getTheme = (): DefaultTheme => {
    if (typeof window !== 'undefined') {
      const stored = localStorage.getItem('theme')
      return stored === 'light' ? LightTheme : DarkTheme
    }

    return LightTheme
  }

  const [theme, setTheme] = React.useState<DefaultTheme>(getTheme())
  const value = React.useMemo(
    () => ({
      theme,
      setTheme,
    }),
    [theme]
  )

  React.useEffect(() => {
    if (typeof window !== 'undefined') {
      localStorage.setItem('theme', theme === LightTheme ? 'light' : 'dark')
    }
  }, [theme])

  return (
    <Context.Provider value={value}>
      <StyledComponentsThemeProvider theme={theme}>{children}</StyledComponentsThemeProvider>
    </Context.Provider>
  )
}

export default Context
