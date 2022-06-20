import type { StandardProperties } from 'csstype'

import styled from 'styled-components'

type NavbarItemsProps = {
  align?: StandardProperties['justifyContent']
}

const NavbarItems = styled.ul<NavbarItemsProps>`
  display: flex;
  flex-direction: row;
  flex-grow: 1;
  justify-content: ${(props) => props.align ?? 'flex-start'};
  margin: 0;
  padding: 0;
  width: 0;
`

NavbarItems.defaultProps = {
  align: 'flex-start',
}

export default NavbarItems
