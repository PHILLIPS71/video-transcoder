import React from 'react'
import { Bell, Menu, X } from 'lucide-react'

import { Navbar } from '@giantnodes/ui'

import ThemeSwitcher from '@/layouts/components/widgets/ThemeSwitcher'
import DefaultLayout from '@/layouts/DefaultLayout'

const NavigationBar = () => (
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

            <Navbar.Item>
              <Navbar.Brand>
                <img src="/images/giantnodes-logo.png" alt="giantnodes logo" />
              </Navbar.Brand>
            </Navbar.Item>
          </Navbar.Items>

          <Navbar.Items align="flex-end">
            <Navbar.Item>
              <ThemeSwitcher />
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

export default NavigationBar
