import type { DefaultTheme } from 'styled-components'

import React from 'react'
import { ThemeProvider } from 'styled-components'

import { DarkTheme, LightTheme } from '@giantnodes/ui'

type ThemeProps = {
  children: React.ReactElement[]
}

export type ThemeContext = {
  theme: DefaultTheme
  setTheme: React.Dispatch<React.SetStateAction<DefaultTheme>>
}

type ThemeComponent = React.FC<ThemeProps> & {
  Context: React.Context<ThemeContext>
}

const Context = React.createContext<ThemeContext>({
  theme: LightTheme,
  setTheme: () => null,
} as ThemeContext)

const Theme: ThemeComponent = ({ children }) => {
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
      <ThemeProvider theme={theme}>{children}</ThemeProvider>
    </Context.Provider>
  )
}

Theme.Context = Context

export default Theme
