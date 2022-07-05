import type { GridContext } from '@/blocks/grid/Grid'
import type { Responsive } from '@/utilities/responsive'
import type { Property } from 'csstype'
import type { DefaultTheme, ThemedStyledProps } from 'styled-components'

import React from 'react'
import styled from 'styled-components'

import Grid from '@/blocks/grid/Grid'
import { getMediaQueries, getResponsiveNumber, getResponsiveValue } from '@/utilities/responsive'

type ColumnProps = Partial<GridContext> & {
  children: React.ReactElement | React.ReactElement[]
  align?: Property.JustifyContent
  order?: Responsive<number>
  skip?: Responsive<number>
  span?: Responsive<number>
}

const getColumnStyles = ({
  theme,
  align,
  order,
  skip,
  span,
  columns,
  gaps,
  gutters,
}: ThemedStyledProps<Required<ColumnProps>, DefaultTheme>): Record<string, string> => {
  const mediaQueries = getMediaQueries(theme.layout.breakpoints)
  const styles = mediaQueries.reduce(
    (acc, cur, idx) => {
      if (getResponsiveNumber(span, idx) === 0) {
        return {
          ...acc,
          [cur]: {
            width: '0',
            paddingLeft: '0',
            paddingRight: '0',
            marginLeft: '0',
            marginRight: '0',
            display: 'none',
          },
        }
      }
      return {
        ...acc,
        [cur]: {
          display: 'block',
          width: `${
            (100 / getResponsiveNumber(columns, idx)) *
            Math.min(getResponsiveNumber(span, idx), getResponsiveNumber(columns, idx))
          }%`,
          marginLeft: `${
            (100 / getResponsiveNumber(columns, idx)) *
            Math.min(getResponsiveNumber(skip, idx), getResponsiveNumber(columns, idx) - 1)
          }%`,
          paddingLeft: `${getResponsiveNumber(gutters, idx) / 2}px`,
          paddingRight: `${getResponsiveNumber(gutters, idx) / 2}px`,
          marginBottom: `${getResponsiveNumber(gaps, idx)}px`,
          alignSelf: getResponsiveValue(align, idx),
          justifyContent: getResponsiveValue(align, idx),
          order: getResponsiveNumber(order, idx),
        },
      }
    },
    {
      width: '100%',
      paddingLeft: `${getResponsiveNumber(gutters, 0) / 2}px`,
      paddingRight: `${getResponsiveNumber(gutters, 0) / 2}px`,
      marginBottom: `${getResponsiveNumber(gaps, 0)}px`,
      alignSelf: getResponsiveValue(align, 0),
      justifyContent: getResponsiveValue(align, 0),
      order: getResponsiveNumber(order, 0).toString(),
    }
  )

  return styles
}

const ColumnElement = styled.div<Required<ColumnProps>>`
  box-sizing: border-box;

  ${getColumnStyles}
`

const GridColumn: React.FC<ColumnProps> = ({ children, align, order, skip, span }) => (
  <Grid.Consumer>
    {(context) => (
      <ColumnElement
        align={align || 'flex-start'}
        columns={context.columns}
        gaps={context.gaps}
        gutters={context.gutters}
        order={order || [1, 1, 1, 1]}
        skip={skip || [0, 0, 0, 0]}
        span={span || [1, 1, 1, 1]}
      >
        {children}
      </ColumnElement>
    )}
  </Grid.Consumer>
)

GridColumn.defaultProps = {
  align: 'flex-start',
  order: [1, 1, 1, 1],
  skip: [0, 0, 0, 0],
  span: [1, 1, 1, 1],
}

export default GridColumn
