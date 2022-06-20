import React from 'react'
import { Bell, Menu, X } from 'react-feather'

import { Navbar } from '@giantnodes/ui'

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

            <Navbar.Item onClick={() => context.setSidebarOpen(!context.isSidebarOpen)}>Giantnodes</Navbar.Item>
          </Navbar.Items>

          <Navbar.Items align="flex-end">
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
