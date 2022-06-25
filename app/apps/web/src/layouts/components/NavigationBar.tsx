import type { ThemeContext } from '@/Theme'

import React from 'react'
import { Bell, Menu, X } from 'react-feather'

import { DarkTheme, LightTheme, Navbar } from '@giantnodes/ui'

import ThemeToggleWidget from '@/layouts/components/widgets/ThemeToggle'
import DefaultLayout from '@/layouts/DefaultLayout'
import Theme from '@/Theme'

const NavigationBar = () => {
  const { theme, setTheme } = React.useContext<ThemeContext>(Theme.Context)
  const [isChecked, setChecked] = React.useState<boolean>(theme !== LightTheme)

  const onThemeWidgetClick = (event: React.ChangeEvent<HTMLInputElement>) => {
    setChecked(event.target.checked)
    setTheme(event.target.checked ? DarkTheme : LightTheme)
  }

  return (
    <DefaultLayout.Consumer>
      {(context) => (
        <Navbar>
          <Navbar.Content>
            <Navbar.Items align="flex-start">
              {context.isMobile && (
                <Navbar.Item onClick={() => context.setSidebarOpen(!context.isSidebarOpen)}>
                  {context.isSidebarOpen ? <X size={20} /> : <Menu size={20} />}
                </Navbar.Item>
              )}

              {!context.isMobile && <Navbar.Item />}

              <Navbar.Item>Giantnodes</Navbar.Item>
            </Navbar.Items>

            <Navbar.Items align="flex-end">
              <Navbar.Item>
                <ThemeToggleWidget checked={isChecked} onChange={onThemeWidgetClick} />
              </Navbar.Item>
              <Navbar.Item>
                <Bell size={20} />
              </Navbar.Item>
            </Navbar.Items>
          </Navbar.Content>
        </Navbar>
      )}
    </DefaultLayout.Consumer>
  )
}

export default NavigationBar
