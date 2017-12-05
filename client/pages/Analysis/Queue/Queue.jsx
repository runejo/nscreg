import React from 'react'
import { func, arrayOf, bool, shape, number, string } from 'prop-types'
import { Segment, Table } from 'semantic-ui-react'

import { getDate, formatDate } from 'helpers/dateHelper'
import Paginate from 'components/Paginate'
import Item from './Item'
import SearchForm from './SearchForm'

const headerKeys = [
  'Comment',
  'ServerEndPeriod',
  'ServerStartPeriod',
  'UserEndPeriod',
  'UserStartPeriod',
  'User',
]

const Queue = ({ items, localize, totalCount, fetching }) => (
  <div>
    <h2>{}</h2>
    <Segment loading={fetching}>
      <SearchForm
        searchQuery={{}}
        onChange={() => {}}
        onSubmit={() => {}}
        localize={localize}
      />
      <br />
      <Paginate totalCount={Number(totalCount)}>
        <Table selectable size="small" className="wrap-content">
          <Table.Header>
            <Table.Row>
              {headerKeys.map(key => <Table.HeaderCell key={key} content={key} />)}
              <Table.HeaderCell />
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {items.map(item => <Item key={item.id} data={item} localize={localize} />)}
          </Table.Body>
        </Table>
      </Paginate>
    </Segment>
  </div>
)

Queue.propTypes = {
  localize: func.isRequired,
  totalCount: number.isRequired,
  actions: shape({
    updateQueueFilter: func.isRequired,
    setQuery: func.isRequired,
  }).isRequired,
  fetching: bool.isRequired,
  formData: shape({}).isRequired,
  query: shape({
    status: string,
    dateTo: string,
  }),
}

Queue.defaultProps = {
  query: {
    status: 'any',
    dateTo: formatDate(getDate()),
  },
}

export default Queue
