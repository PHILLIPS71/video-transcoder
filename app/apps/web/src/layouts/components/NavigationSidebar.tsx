import Link from 'next/link'
import React from 'react'
import { Home, Search } from 'react-feather'

import { Sidebar } from '@giantnodes/ui'

import DefaultLayout from '@/layouts/DefaultLayout'

const NavigationSidebar = () => (
  <DefaultLayout.Consumer>
    {(context) => (
      <Sidebar expanded={context.isSidebarOpen} position={context.isMobile ? 'absolute' : 'relative'}>
        <Sidebar.Content>
          <Sidebar.Items>
            <Sidebar.Item>
              <Link href="/" passHref>
                <Home size={20} />
              </Link>
            </Sidebar.Item>
          </Sidebar.Items>

          <Sidebar.Items align="flex-end">
            <Sidebar.Item>
              <Search size={20} />
            </Sidebar.Item>
          </Sidebar.Items>
        </Sidebar.Content>
      </Sidebar>
    )}
  </DefaultLayout.Consumer>
)

export default NavigationSidebar
