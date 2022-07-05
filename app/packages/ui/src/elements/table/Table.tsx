import React, { createContext } from 'react'
import styled from 'styled-components'

import TableBody from '@/elements/table/TableBody'
import TableCell from '@/elements/table/TableCell'
import TableHead from '@/elements/table/TableHead'
import TableHeading from '@/elements/table/TableHeading'
import TableRow from '@/elements/table/TableRow'

export type TableContext = {
  bordered?: boolean
  hoverable?: boolean
}

type TableProps = TableContext & {
  children: React.ReactElement[]
}

const { Provider, Consumer } = createContext<TableContext>({
  bordered: false,
  hoverable: false,
})

type TableComponent = React.FC<TableProps> & {
  Consumer: React.Consumer<TableContext>
  Head: typeof TableHead
  Body: typeof TableBody
  Cell: typeof TableCell
  Heading: typeof TableHeading
  Row: typeof TableRow
}

const TableElement = styled.table<TableProps>`
  border: ${(props) => (props.bordered ? `1px solid ${props.theme.colors.text.tertiary}` : 'unset')};
  border-collapse: collapse;
  border-radius: 1em;
  border-spacing: 0;
  width: 100%;
  overflow: hidden;
`

const Table: TableComponent = ({ children, ...props }) => (
  <Provider
    value={{
      bordered: props.bordered,
      hoverable: props.hoverable,
    }}
  >
    <TableElement {...props}>{children}</TableElement>
  </Provider>
)

Table.Consumer = Consumer
Table.Body = TableBody
Table.Cell = TableCell
Table.Head = TableHead
Table.Heading = TableHeading
Table.Row = TableRow

export default Table
