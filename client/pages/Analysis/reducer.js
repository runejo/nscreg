import { createReducer } from 'redux-act'
import { toUtc } from 'helpers/dateHelper'

import actions from './actions'

const defaultState = {
  queue: {
    formData: {},
    items: [],
    totalCount: 0,
    fetching: false,
    error: undefined,
  },
  logs: {
    formData: {},
    items: [],
    totalCount: 0,
    fetching: false,
    error: undefined,
  },
  create: {
    item: {
      dateFrom: toUtc(new Date()),
      dateTo: toUtc(new Date()),
      comment: '',
    },
  },
  details: {
    logEntry: undefined,
    properties: undefined,
    permissions: undefined,
    errors: undefined,
    fetching: false,
  },
}

const queueHandlers = {
  [actions.fetchQueueSucceeded]: (state, data) => ({
    ...state,
    queue: {
      ...state.queue,
      items: data.items,
      totalCount: data.totalCount,
      fetching: false,
      error: undefined,
    },
  }),
  [actions.fetchQueueFailed]: (state, data) => ({
    ...state,
    queue: {
      ...state.queue,
      data: undefined,
      fetching: false,
      error: data,
    },
  }),
  [actions.fetchQueueStarted]: state => ({
    ...state,
    queue: {
      ...state.queue,
      fetching: true,
      error: undefined,
    },
  }),
  [actions.updateQueueFilter]: (state, data) => ({
    ...state,
    queue: {
      ...state.queue,
      formData: { ...state.formData, ...data },
    },
  }),
  [actions.editQueueItem]: (state, data) => ({
    ...state,
    create: {
      ...state.create,
      item: {
        ...state.create.item,
        [data.name]: data.value,
      },
    },
  }),
  [actions.fetchAnalysisLogsSucceeded]: (state, data) => ({
    ...state,
    logs: {
      ...state.logs,
      items: data.items,
      totalCount: data.totalCount,
      fetching: false,
      error: undefined,
    },
  }),
  [actions.fetchAnalysisLogsFailed]: (state, data) => ({
    ...state,
    logs: {
      ...state.logs,
      data: undefined,
      fetching: false,
      error: data,
    },
  }),
  [actions.fetchAnalysisLogsStarted]: state => ({
    ...state,
    logs: {
      ...state.logs,
      fetching: true,
      error: undefined,
    },
  }),
}

const detailsHandlers = {
  [actions.fetchDetailsStarted]: state => ({
    ...state,
    details: {
      ...defaultState.details,
      fetching: true,
      errors: undefined,
    },
  }),
  [actions.fetchDetailsSucceeded]: (state, payload) => ({
    ...state,
    details: {
      ...payload,
      fetching: false,
      errors: undefined,
    },
  }),
  [actions.fetchDetailsFailed]: (state, errors) => ({
    ...state,
    details: {
      ...defaultState.details,
      fetching: false,
      errors,
    },
  }),
  [actions.clearDetails]: state => ({
    ...state,
    details: defaultState.details,
  }),
}

export default createReducer(
  {
    ...queueHandlers,
    ...detailsHandlers,
    [actions.clear]: () => defaultState,
  },
  defaultState,
)
