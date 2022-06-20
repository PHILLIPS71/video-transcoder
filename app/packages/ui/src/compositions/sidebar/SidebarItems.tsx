import type { StandardProperties } from 'csstype'

import styled from 'styled-components'

type SidebarItemsProps = {
  align?: StandardProperties['justifyContent']
}

const SidebarItems = styled.ul<SidebarItemsProps>`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
  justify-content: ${(props) => props.align ?? 'flex-start'};
  margin: 0;
  padding: 0;
`

SidebarItems.defaultProps = {
  align: 'flex-start',
}

export default SidebarItems
