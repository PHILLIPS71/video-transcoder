import React from 'react'
import styled from 'styled-components'

import NavbarBrand from '@/compositions/navbar/NavbarBrand'
import NavbarContent from '@/compositions/navbar/NavbarContent'
import NavbarItem from '@/compositions/navbar/NavbarItem'
import NavbarItems from '@/compositions/navbar/NavbarItems'

type NavbarProps = {
  children: React.ReactNode
  expanded?: boolean
}

type NavbarComponent = React.FC<NavbarProps> & {
  Brand: typeof NavbarBrand
  Content: typeof NavbarContent
  Items: typeof NavbarItems
  Item: typeof NavbarItem
}

const NavbarElement = styled.nav<NavbarProps>`
  background-color: ${(props) => props.theme.colours.background.secondary};
  border-bottom: 1px solid ${(props) => props.theme.colours.background.tertiary};
  box-shadow: none;
  display: flex;
  flex-direction: row;
  height: 60px;
  position: relative;
  transform: ${(props) => (props.expanded ? 'translateX(0)' : 'translateX(-100%)')};
  transition: all 0.3s;
  width: 100%;
  z-index: 99;
`

const Navbar: NavbarComponent = ({ children, ...rest }) => <NavbarElement {...rest}>{children}</NavbarElement>

Navbar.Brand = NavbarBrand
Navbar.Content = NavbarContent
Navbar.Items = NavbarItems
Navbar.Item = NavbarItem

Navbar.defaultProps = {
  expanded: true,
}

export default Navbar
