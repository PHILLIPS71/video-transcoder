import type {
  DisplayProps,
  FlexProps,
  LayoutProps,
  MarginProps,
  PaddingProps,
  PositionProps,
  TypographyProps,
} from 'styled-foundations'

import styled from 'styled-components'
// eslint-disable-next-line import/no-extraneous-dependencies
import { color, display, flex, layout, margin, padding, position, typography } from 'styled-foundations'

type BlockAttributes = {
  as?: string
}

type BlockProps = DisplayProps & FlexProps & LayoutProps & MarginProps & PaddingProps & PositionProps & TypographyProps

const Block = styled.div.attrs<BlockAttributes>(({ as }) => ({ as: as ?? 'div' }))<BlockProps>`
  ${color}
  ${display}
  ${flex}
  ${layout}
  ${margin}
  ${padding}
  ${position}
  ${typography}
`

export default Block
