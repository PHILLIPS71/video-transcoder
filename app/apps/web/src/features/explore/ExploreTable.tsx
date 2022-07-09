import type { GetDirectoryContentsQuery } from '@/__generated__/graphql-types'

import { Trans } from '@lingui/macro'
import filesize from 'filesize'
import Link from 'next/link'
import { useRouter } from 'next/router'
import React from 'react'
import { ArrowLeft, File, Folder } from 'react-feather'

import { Block, Paragraph, Table } from '@giantnodes/ui'

type ExploreTableProps = {
  directory: GetDirectoryContentsQuery['directory_contents']
}

const ExploreTable = ({ directory }: ExploreTableProps) => {
  const { asPath, query } = useRouter()
  const [total, setTotal] = React.useState<number>(0)

  const getBackPath = (): string => {
    const parts = asPath.split('/')
    parts.pop()

    return parts.join('/')
  }

  React.useEffect(() => {
    const size = directory.reduce<number>((accu, node) => {
      // eslint-disable-next-line no-param-reassign
      accu += node?.length ?? 0
      return accu
    }, 0)

    setTotal(size)
  }, [directory])

  return (
    <Table bordered>
      <Table.Head>
        <Table.Row>
          <Table.Heading>
            <Trans>Name</Trans>
          </Table.Heading>
          <Table.Heading textAlign="right">
            <Trans>Size</Trans>
          </Table.Heading>
        </Table.Row>
      </Table.Head>

      <Table.Body>
        <Table.Row>
          <Table.Cell>
            <Block display="flex" alignItems="center">
              {query.slug != null && (
                <Link href={getBackPath()}>
                  <ArrowLeft size={18} cursor="pointer" />
                </Link>
              )}
            </Block>
          </Table.Cell>
          <Table.Cell textAlign="right">
            <Paragraph color="text.secondary">{filesize(total)}</Paragraph>
          </Table.Cell>
        </Table.Row>

        {directory.map((node) => (
          <Table.Row key={node.path}>
            <Table.Cell>
              <>
                {node.__typename === 'FileSystemDirectory' && (
                  <Block display="flex" alignItems="center" gridGap="16px">
                    <Folder size={18} />
                    <Link href={`${asPath}/${node.name}/`}>{node.name}</Link>
                  </Block>
                )}
                {node.__typename === 'FileSystemFile' && (
                  <Block display="flex" alignItems="center" gridGap="16px">
                    <File size={18} />
                    <Paragraph>{node.name}</Paragraph>
                  </Block>
                )}
              </>
            </Table.Cell>
            <Table.Cell textAlign="right">
              <Paragraph color="text.secondary">{node.length ? filesize(node.length) : ''}</Paragraph>
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table>
  )
}

export default ExploreTable
