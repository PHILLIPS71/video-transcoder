import styled from 'styled-components'

const SidebarItem = styled.li`
  align-items: center;
  color: ${(props) => props.theme.colours.text.secondary};
  cursor: pointer;
  display: flex;
  justify-content: center;
  min-height: 60px;
  position: relative;
  width: 100%;

  &:hover {
    color: ${(props) => props.theme.colours.primary};
  }
`

export default SidebarItem
