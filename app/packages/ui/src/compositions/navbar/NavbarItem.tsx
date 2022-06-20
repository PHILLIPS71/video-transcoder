import styled from 'styled-components'

const NavbarItem = styled.li`
  align-items: center;
  color: ${(props) => props.theme.colours.text.secondary};
  cursor: pointer;
  display: flex;
  justify-content: center;
  min-height: 60px;
  min-width: 70px;
  position: relative;

  &:hover {
    color: ${(props) => props.theme.colours.primary};
  }
`

export default NavbarItem
