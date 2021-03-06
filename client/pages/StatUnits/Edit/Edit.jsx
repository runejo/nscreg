import React from 'react'
import PropTypes from 'prop-types'
import { Button, Icon, Modal, Checkbox, TextArea, Grid } from 'semantic-ui-react'

import ConnectedForm from './ConnectedForm'
import styles from './styles.pcss'

const { func, number, shape, string } = PropTypes

const Mandatory = '1'
const NotMandatory = '2'

// TODO: revise form submit logic
// `formActions` in state is a hacky solution to intercepted submit event of the form
// normally, this behavior should be handled by generator in action, or similar flow
// e.g. some long-running process, so submit event would not lose its context and
// there will be no need to store `formActions` anywhere
class EditStatUnitPage extends React.Component {
  static propTypes = {
    type: number.isRequired,
    regId: number.isRequired,
    submitStatUnit: func.isRequired,
    localize: func.isRequired,
    goBack: func.isRequired,
    errors: shape({
      message: string,
    }),
  }

  static defaultProps = {
    errors: undefined,
  }

  state = {
    changeReason: Mandatory,
    editComment: '',
    statUnitToSubmit: undefined,
    formikBag: undefined,
  }

  handleSubmit = () => {
    const { type, regId, submitStatUnit } = this.props
    const { changeReason, editComment, statUnitToSubmit, formikBag } = this.state
    this.setState({ statUnitToSubmit: undefined, formikBag: undefined }, () => {
      submitStatUnit(type, { ...statUnitToSubmit, regId, changeReason, editComment }, formikBag)
    })
  }

  handleModalEdit = (_, { name, value }) => {
    this.setState({ [name]: value })
  }

  showModal = (statUnitToSubmit, formikBag) => {
    this.setState({ statUnitToSubmit, formikBag })
  }

  hideModal = () => {
    this.state.formikBag.succeeded(false)
    this.setState({ statUnitToSubmit: undefined, formikBag: undefined })
  }

  render() {
    const { localize, type, regId, goBack, errors } = this.props
    const { statUnitToSubmit, editComment, changeReason } = this.state
    const isMandatory = changeReason === Mandatory
    const header = isMandatory ? 'CommentIsMandatory' : 'CommentIsNotMandatory'
    return (
      <div className={styles.root}>
        <ConnectedForm
          onSubmit={this.showModal}
          localize={localize}
          errors={errors}
          type={type}
          regId={regId}
          showSummary
          goBack={goBack}
        />
        <Modal open={statUnitToSubmit !== undefined}>
          <Modal.Header content={localize(header)} />
          <Modal.Content>
            <Grid>
              <Grid.Row>
                <Grid.Column width="3">
                  <Checkbox
                    name="changeReason"
                    value={Mandatory}
                    checked={isMandatory}
                    onChange={this.handleModalEdit}
                    label={localize('Editing')}
                    radio
                  />
                  <Checkbox
                    name="changeReason"
                    value={NotMandatory}
                    checked={!isMandatory}
                    onChange={this.handleModalEdit}
                    label={localize('Correcting')}
                    radio
                  />
                  <br key="modal_reason_radio_br" />
                  <Icon name={isMandatory ? 'edit' : 'write'} size="massive" />
                </Grid.Column>
                <Grid.Column width="13" className="ui form">
                  <TextArea
                    name="editComment"
                    value={editComment}
                    onChange={this.handleModalEdit}
                    rows={8}
                  />
                </Grid.Column>
              </Grid.Row>
            </Grid>
          </Modal.Content>
          <Modal.Actions>
            <Button.Group>
              <Button onClick={this.hideModal} content={localize('ButtonCancel')} negative />
              <Button
                onClick={this.handleSubmit}
                disabled={isMandatory && editComment === ''}
                content={localize('Submit')}
                positive
              />
            </Button.Group>
          </Modal.Actions>
        </Modal>
      </div>
    )
  }
}

export default EditStatUnitPage
