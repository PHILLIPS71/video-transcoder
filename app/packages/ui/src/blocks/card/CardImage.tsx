import styled from 'styled-components'

const CardImage = styled.div`
  display: block;
  position: relative;

  &:first-child {
    border-top-left-radius: 0.25rem;
    border-top-right-radius: 0.25rem;
  }

  &:last-child {
    border-bottom-left-radius: 0.25rem;
    border-bottom-right-radius: 0.25rem;
  }
`

export default CardImage
