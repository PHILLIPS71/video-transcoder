import type { TableContext } from '@/elements/table/Table'

import React from 'react'
import styled from 'styled-components'

import Table from '@/elements/table/Table'

type TableRowProps = {
  children: React.ReactElement | React.ReactElement[]
}

const TableRowElement = styled.tr<TableContext>`
  border: ${(props) => (props.bordered ? `1px solid ${props.theme.colors.background.tertiary}` : 'unset')};
`

const TableRow: React.FC<TableRowProps> = ({ children }) => (
  <Table.Consumer>
    {(context) => (
      <TableRowElement bordered={context.bordered} hoverable={context.hoverable}>
        {children}
      </TableRowElement>
    )}
  </Table.Consumer>
)

export default TableRow
