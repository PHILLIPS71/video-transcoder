import React from 'react'
import styled from 'styled-components'

import CardBody from '@/blocks/card/CardBody'
import CardFooter from '@/blocks/card/CardFooter'
import CardHeader from '@/blocks/card/CardHeader'
import CardImage from '@/blocks/card/CardImage'

type CardProps = {
  children: React.ReactElement | React.ReactElement[]
  outlined?: boolean
}

type CardComponent = React.FC<CardProps> & {
  Header: typeof CardHeader
  Body: typeof CardBody
  Footer: typeof CardFooter
  Image: typeof CardImage
}

const CardElement = styled.div<CardProps>`
  background-color: ${(props) => props.theme.colors.background.tertiary};
  border-radius: 16px;
  padding: 8px 16px;

  &:not(:last-child) {
    margin-bottom: 16px;
  }
`

const Card: CardComponent = ({ children, ...rest }) => <CardElement outlined={rest.outlined}>{children}</CardElement>

Card.Header = CardHeader
Card.Body = CardBody
Card.Footer = CardFooter
Card.Image = CardImage

export default Card
