import type { ColorProps } from 'styled-foundations'

import styled from 'styled-components'
import { color } from 'styled-foundations'

type ParagraphProps = ColorProps

const Paragraph = styled.p<ParagraphProps>`
  color: ${(props) => props.theme.colors.text.primary};
  margin: 0;

  ${color}
`

export default Paragraph
