import styled from 'styled-components'

const CardFooter = styled.footer`
  background-color: ${(props) => props.theme.colors.white};
  align-items: stretch;
  display: flex;

  &:first-child {
    border-top-left-radius: 0.25rem;
    border-top-right-radius: 0.25rem;
  }

  &:last-child {
    border-bottom-left-radius: 0.25rem;
    border-bottom-right-radius: 0.25rem;
  }
`

export default CardFooter
