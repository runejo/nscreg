import { createAction } from 'redux-act'
import { push } from 'react-router-redux'
import dispatchRequest from 'helpers/request'
import { pipe } from 'ramda'

const fetchQueueStarted = createAction('fetch Analysis Queue started')
const fetchQueueFailed = createAction('fetch Analysis Queue failed')
const fetchQueueSucceeded = createAction('fetch Analysis Queue successed')
const updateQueueFilter = createAction('update search Analysis Queue form')
const clear = createAction('clear filter on DataSourceQueue')

const fetchQueue = queryParams =>
  dispatchRequest({
    url: '/api/analysisqueue',
    queryParams,
    onSuccess: (dispatch, resp) => {
      dispatch(fetchQueueSucceeded({ ...resp }))
    },
    onFail: (dispatch, errors) => {
      dispatch(fetchQueueFailed(errors))
    },
  })

export const queue = {
  fetchQueueStarted,
  fetchQueueSucceeded,
  fetchQueueFailed,
  fetchQueue,
  clear,
}

export default {
  fetchQueueStarted,
  fetchQueueSucceeded,
  fetchQueueFailed,
  fetchQueue,
  clear,
}
