import type { DisplayProps, FlexProps, LayoutProps, MarginProps, PaddingProps, PositionProps } from 'styled-foundations'

import styled from 'styled-components'
// eslint-disable-next-line import/no-extraneous-dependencies
import { display, flex, layout, margin, padding, position } from 'styled-foundations'

type BlockProps = DisplayProps & FlexProps & LayoutProps & MarginProps & PaddingProps & PositionProps

const Block = styled.div<BlockProps>`
  ${display}
  ${flex}
  ${layout}
  ${margin}
  ${padding}
  ${position}
`

export default Block
