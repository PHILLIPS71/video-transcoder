import type { PositionProps } from 'styled-foundations'

import React from 'react'
import styled from 'styled-components'
// eslint-disable-next-line import/no-extraneous-dependencies
import { position } from 'styled-foundations'

import SidebarContent from '@/compositions/sidebar/SidebarContent'
import SidebarItem from '@/compositions/sidebar/SidebarItem'
import SidebarItems from '@/compositions/sidebar/SidebarItems'

type SidebarProps = PositionProps & {
  children: React.ReactNode
  expanded?: boolean
}

type SidebarComponent = React.FC<SidebarProps> & {
  Content: typeof SidebarContent
  Items: typeof SidebarItems
  Item: typeof SidebarItem
}

const SidebarElement = styled.aside<SidebarProps>`
  background-color: ${(props) => props.theme.colours.background.secondary};
  border-right: 1px solid ${(props) => props.theme.colours.background.tertiary};
  display: flex;
  flex-direction: column;
  height: 100%;
  min-width: 70px;
  position: relative;
  transform: ${(props) => (props.expanded ? 'translateX(0)' : 'translateX(-100%)')};
  transition: all 0.3s;
  width: 70px;
  z-index: 99;

  ${position}
`

const Sidebar: SidebarComponent = ({ children, ...rest }) => <SidebarElement {...rest}>{children}</SidebarElement>

Sidebar.Content = SidebarContent
Sidebar.Items = SidebarItems
Sidebar.Item = SidebarItem

export default Sidebar
