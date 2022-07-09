import type { TableContext } from '@/elements/table/Table'
import type { TypographyProps } from 'styled-foundations'

import React from 'react'
import styled from 'styled-components'
import { typography } from 'styled-foundations'

import Table from '@/elements/table/Table'

type TableCellProps = React.HTMLAttributes<HTMLTableCellElement> &
  TableContext &
  TypographyProps & {
    children: string | React.ReactElement
  }

const TableCellElement = styled.td<TableCellProps>`
  border: ${(props) => (props.bordered ? `1px solid ${props.theme.colors.text.tertiary}` : 'unset')};
  padding: 8px 16px;
  vertical-align: middle;

  &:hover {
    background-color: ${(props) => (props.hoverable ? props.theme.colors.background.secondary : 'unset')};
  }

  ${typography}
`

const TableCell: React.FC<TableCellProps> = ({ children, ...rest }) => (
  <Table.Consumer>{() => <TableCellElement {...rest}>{children}</TableCellElement>}</Table.Consumer>
)

export default TableCell
