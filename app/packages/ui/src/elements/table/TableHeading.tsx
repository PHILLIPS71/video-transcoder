import type { TypographyProps } from 'styled-foundations'

import styled from 'styled-components'
import { typography } from 'styled-foundations'

type TableHeadProps = TypographyProps

const TableHead = styled.th<TableHeadProps>`
  padding: 8px 16px;
  vertical-align: middle;

  ${typography}
`

export default TableHead
