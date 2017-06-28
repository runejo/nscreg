import React from 'react'
import { func } from 'prop-types'
import { Link } from 'react-router'
import { Button, Form, Loader, Icon } from 'semantic-ui-react'
import R from 'ramda'

import DataAccess from 'components/DataAccess'
import ActivityTree from 'components/ActivityTree'
import FunctionalAttributes from 'components/FunctionalAttributes'
import { wrapper } from 'helpers/locale'
import styles from './styles.pcss'

class Edit extends React.Component {
  static propTypes = {
    editForm: func.isRequired,
    fetchRole: func.isRequired,
    fetchActivityTree: func.isRequired,
    submitRole: func.isRequired,
    localize: func.isRequired,
  }

  componentDidMount() {
    this.props.fetchActivityTree()
    this.props.fetchRole(this.props.id)
  }

  shouldComponentUpdate(nextProps, nextState) {
    return this.props.localize.lang !== nextProps.localize.lang
      || !R.equals(this.props, nextProps)
      || !R.equals(this.state, nextState)
  }

  setRegion = (region) => {
    this.props.editForm({ name: 'region', value: region })
  }

  setActivities = (activities) => {
    this.props.editForm({ name: 'activiyCategoryIds', value: activities.filter(x => x !== 'all') })
  }

  handleEdit = (e, { name, value }) => {
    this.props.editForm({ name, value })
  }

  handleSubmit = (e) => {
    e.preventDefault()
    this.props.submitRole({ ...this.props.role })
  }

  handleAccessToSystemFunctionsChange = e => this.props.editForm({
    name: e.name,
    value: e.checked
      ? [...this.props.role.accessToSystemFunctions, e.value]
      : this.props.role.accessToSystemFunctions.filter(x => x !== e.value),
  })

  render() {
    const { role, activityTree, localize } = this.props
    return (
      <div className={styles.roleEdit}>
        {role === undefined
          ? <Loader active />
          : <Form className={styles.form} onSubmit={this.handleSubmit}>
            <h2>{localize('EditRole')}</h2>
            <Form.Input
              value={role.name}
              onChange={this.handleEdit}
              name="name"
              label={localize('RoleName')}
              placeholder={localize('RoleNamePlaceholder')}
              required
            />
            <Form.Input
              value={role.description}
              onChange={this.handleEdit}
              name="description"
              label={localize('Description')}
              placeholder={localize('RoleDescriptionPlaceholder')}
            />
            <DataAccess
              value={role.standardDataAccess}
              name="standardDataAccess"
              label={localize('DataAccess')}
              onChange={this.handleEdit}
            />
            {activityTree &&
            <ActivityTree
              name="activiyCategoryIds"
              label="ActivityCategoryLookup"
              dataTree={activityTree}
              checked={role.activiyCategoryIds}
              callBack={this.setActivities}
            />
            }
            <FunctionalAttributes
              label={localize('AccessToSystemFunctions')}
              value={role.accessToSystemFunctions}
              onChange={this.handleAccessToSystemFunctionsChange}
              name="accessToSystemFunctions"
            />
            <Button
              as={Link}
              to="/roles"
              content={localize('Back')}
              icon={<Icon size="large" name="chevron left" />}
              size="small"
              color="grey"
              type="button"
            />
            <Button
              content={localize('Submit')}
              className={styles.sybbtn}
              type="submit"
              primary
            />
          </Form>}
      </div>
    )
  }
}

export default wrapper(Edit)
