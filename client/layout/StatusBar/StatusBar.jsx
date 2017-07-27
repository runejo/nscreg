import React from 'react'
import PropTypes from 'prop-types'
import { Button } from 'semantic-ui-react'

import withOnMount from 'components/withOnMount'
import Failed from './Failed'
import Loading from './Loading'
import Success from './Success'
import styles from './styles.pcss'

const createMessage = (id, code, message, dismiss) => {
  let Message
  const onMount = () => setTimeout(dismiss, 3000)
  switch (code) {
    case -1:
      Message = withOnMount(Failed)
      break
    case 1:
      return Loading({ key: id, message })
    case 2:
      Message = withOnMount(Success)
      break
    default:
      return null
  }
  return <Message key={id} message={message} onMount={onMount} dismiss={dismiss} />
}

const StatusBar = ({ status, dismiss, dismissAll, localize }) => {
  const renderMessage = ({ id, code, message }) =>
    createMessage(id, code, localize(message), () => dismiss(id))
  return (
    <div className={styles.root}>
      {status !== undefined && status.map && status.map(renderMessage)}
      {status.length > 1 && status.map
        && <Button
          onClick={dismissAll}
          className={styles.close}
          color="grey"
          icon="remove"
          basic
        />}
    </div>
  )
}

StatusBar.propTypes = {
  status: PropTypes.arrayOf(PropTypes.shape({})).isRequired,
  localize: PropTypes.func.isRequired,
  dismiss: PropTypes.func.isRequired,
  dismissAll: PropTypes.func.isRequired,
}

export default StatusBar
