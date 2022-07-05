import type { TextAlignProps } from 'styled-foundations'

import styled from 'styled-components'
import { textAlign } from 'styled-foundations'

type TableHeadProps = TextAlignProps

const TableHead = styled.th<TableHeadProps>`
  padding: 8px 16px;
  vertical-align: middle;

  ${textAlign}
`

export default TableHead
