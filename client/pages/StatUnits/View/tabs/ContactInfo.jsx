import React from 'react'
import { shape, func, string, number, oneOfType, arrayOf } from 'prop-types'
import { Label, Grid, Header, Segment } from 'semantic-ui-react'

import { PersonsField } from 'components/fields'
import { internalRequest } from 'helpers/request'
import { hasValue } from 'helpers/validation'
import { getNewName } from 'helpers/locale'
import styles from './styles.pcss'

const defaultCode = '41700000000000'
const defaultRegionState = { region: { code: '', name: '' } }

class ContactInfo extends React.Component {
  static propTypes = {
    data: shape({
      emailAddress: string,
      telephoneNo: oneOfType([string, number]),
      address: shape({}).isRequired,
      actualAddress: shape({}),
      persons: arrayOf(shape({})),
    }).isRequired,
    localize: func.isRequired,
    activeTab: string.isRequired,
  }

  state = {
    region:
      hasValue(this.props.data.address) && hasValue(this.props.data.address.region)
        ? { ...this.props.data.address.region }
        : defaultRegionState,
    regionMenu1: {
      options: [],
      value: '',
      submenu: 'regionMenu2',
      substrRule: { start: 3, end: 5 },
    },
    regionMenu2: {
      options: [],
      value: '',
      submenu: 'regionMenu3',
      substrRule: { start: 5, end: 8 },
    },
    regionMenu3: {
      options: [],
      value: '',
      submenu: 'regionMenu4',
      substrRule: { start: 8, end: 11 },
    },
    regionMenu4: { options: [], value: '', submenu: null, substrRule: { start: 11, end: 14 } },
  }

  componentDidMount() {
    const code = hasValue(this.props.data.address)
      ? this.props.data.address.region && this.props.data.address.region.code
        ? this.props.data.address.region.code
        : defaultRegionState.region.code
      : defaultRegionState.region.code
    const menu = 'regionMenu'
    for (let i = 1; i <= 4; i++) {
      const substrStart = this.state[`${menu}${i}`].substrRule.start
      const substrEnd = this.state[`${menu}${i}`].substrRule.end
      this.fetchByPartCode(
        `${menu}${i}`,
        code.substr(0, substrStart),
        defaultCode.substr(substrEnd),
        `${code.substr(0, substrEnd)}${defaultCode.substr(substrEnd)}`,
      )
    }
  }

  fetchByPartCode = (name, start, end, value) =>
    internalRequest({
      url: '/api/regions/getAreasList',
      queryParams: { start, end },
      method: 'get',
      onSuccess: (result) => {
        this.setState(s => ({
          [name]: {
            ...s[name],
            options: result.map(x => ({ key: x.code, value: x.code, text: x.name })),
            value,
          },
        }))
      },
      onFail: () => {
        this.setState(s => ({
          [name]: {
            ...s.name,
            options: [],
            value: '0',
          },
        }))
      },
    })

  render() {
    const { localize, data, activeTab } = this.props
    const { regionMenu1, regionMenu2, regionMenu3, regionMenu4 } = this.state
    const regionFullPaths = data.address && getNewName(data.address.region)
    const regions = regionFullPaths && regionFullPaths.split(',').map(x => x.trim())
    return (
      <div>
        {activeTab !== 'contactInfo' && (
          <Header as="h5" className={styles.heigthHeader} content={localize('ContactInfo')} />
        )}
        <Segment>
          {hasValue(data.actualAddress) ||
          hasValue(data.postalAddress) ||
          hasValue(data.address) ? (
            <Grid divided columns={2}>
              <Grid.Row>
                {hasValue(data.actualAddress) && (
                  <Grid.Column width={8}>
                    <Header as="h5" content={localize('VisitingAddress')} dividing />
                    <Grid doubling>
                      <Grid.Row>
                        {hasValue(data.actualAddress.region) &&
                          hasValue(data.actualAddress.region.fullPath) &&
                          hasValue(data.actualAddress.region.code) && (
                            <React.Fragment>
                              <Grid.Column width={6}>
                                <label className={styles.boldText}>{localize('Region')}</label>
                              </Grid.Column>
                              <Grid.Column width={10}>
                                <Label className={styles.labelStyle} basic size="large">
                                  {`${data.actualAddress.region.code} ${getNewName(data.actualAddress.region)}`}
                                </Label>
                                <br />
                                <br />
                              </Grid.Column>
                            </React.Fragment>
                          )}
                        {hasValue(data.actualAddress.addressPart1) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart1')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.actualAddress.addressPart1}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                        {hasValue(data.actualAddress.addressPart2) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart2')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.actualAddress.addressPart2}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                      </Grid.Row>
                    </Grid>
                  </Grid.Column>
                )}
                {hasValue(data.postalAddress) && (
                  <Grid.Column width={8}>
                    <Header as="h5" content={localize('PostalAddress')} dividing />
                    <Grid doubling>
                      <Grid.Row>
                        {hasValue(data.postalAddress.region) &&
                          hasValue(data.postalAddress.region.fullPath) &&
                          hasValue(data.postalAddress.region.code) && (
                            <React.Fragment>
                              <Grid.Column width={6}>
                                <label className={styles.boldText}>{localize('Region')}</label>
                              </Grid.Column>
                              <Grid.Column width={10}>
                                <Label className={styles.labelStyle} basic size="large">
                                  {`${data.postalAddress.region.code} ${getNewName(data.postalAddress.region)}`}
                                </Label>
                                <br />
                                <br />
                              </Grid.Column>
                            </React.Fragment>
                          )}
                        {hasValue(data.postalAddress.addressPart1) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart1')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.postalAddress.addressPart1}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                        {hasValue(data.postalAddress.addressPart2) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart2')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.postalAddress.addressPart2}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                      </Grid.Row>
                    </Grid>
                  </Grid.Column>
                )}
                {hasValue(data.address) && (
                  <Grid.Column width={8}>
                    <Header as="h5" content={localize('AsRegistered')} dividing />
                    <Grid doubling>
                      <Grid.Row>
                        {hasValue(data.address.region) &&
                          hasValue(data.address.region.fullPath) &&
                          hasValue(data.address.region.code) && (
                            <React.Fragment>
                              <Grid.Column width={6}>
                                <label className={styles.boldText}>{localize('Region')}</label>
                              </Grid.Column>
                              <Grid.Column width={10}>
                                <Label className={styles.labelStyle} basic size="large">
                                  {`${data.address.region.code} ${getNewName(data.address.region)}`}
                                </Label>
                                <br />
                                <br />
                              </Grid.Column>
                            </React.Fragment>
                          )}
                        {hasValue(data.address.addressPart1) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart1')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.address.addressPart1}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                        {hasValue(data.address.addressPart2) && (
                          <React.Fragment>
                            <Grid.Column width={6}>
                              <label className={styles.boldText}>{localize('AddressPart2')}</label>
                            </Grid.Column>
                            <Grid.Column width={10}>
                              <Label className={styles.labelStyle} basic size="large">
                                {data.address.addressPart2}
                              </Label>
                              <br />
                              <br />
                            </Grid.Column>
                          </React.Fragment>
                        )}
                        {(hasValue(data.address.latitude) && data.address.latitude != 0) ||
                        (hasValue(data.address.longitude) && data.address.longitude != 0) ? (
                          <Grid.Column width={16}>
                            <Segment>
                              <Header as="h5" content={localize('GpsCoordinates')} dividing />
                              <Grid doubling>
                                <Grid.Row>
                                  {hasValue(data.address.latitude) && (
                                    <React.Fragment>
                                      <Grid.Column width={6}>
                                        <label className={styles.boldText}>
                                          {localize('Latitude')}
                                        </label>
                                      </Grid.Column>
                                      <Grid.Column width={10}>
                                        <Label className={styles.labelStyle} basic size="large">
                                          {data.address.latitude}
                                        </Label>
                                        <br />
                                        <br />
                                      </Grid.Column>
                                    </React.Fragment>
                                  )}
                                  {hasValue(data.address.longitude) && (
                                    <React.Fragment>
                                      <Grid.Column width={6}>
                                        <label className={styles.boldText}>
                                          {localize('Longitude')}
                                        </label>
                                      </Grid.Column>
                                      <Grid.Column width={10}>
                                        <Label className={styles.labelStyle} basic size="large">
                                          {data.address.longitude}
                                        </Label>
                                        <br />
                                        <br />
                                      </Grid.Column>
                                    </React.Fragment>
                                  )}
                                </Grid.Row>
                              </Grid>
                            </Segment>
                          </Grid.Column>
                        ) : null}
                      </Grid.Row>
                    </Grid>
                  </Grid.Column>
                )}
              </Grid.Row>
            </Grid>
          ) : null}
          <Grid>
            {data.telephoneNo || data.emailAddress ? (
              <Grid.Row>
                {data.telephoneNo && (
                  <Grid.Column width={5}>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('TelephoneNo')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        {data.telephoneNo}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
                {data.emailAddress && (
                  <Grid.Column width={5}>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('EmailAddress')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        {data.emailAddress}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
              </Grid.Row>
            ) : null}
            {hasValue(regions) && (
              <Grid.Row columns={4}>
                {hasValue(regions[0]) && (
                  <Grid.Column>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('RegionLvl1')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        <label className={styles.labelRegion}>{regions[0]}</label>
                        {regionMenu1.value}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
                {hasValue(regions[1]) && (
                  <Grid.Column>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('RegionLvl2')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        <label className={styles.labelRegion}>{regions[1]}</label>
                        {regionMenu2.value}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
                {hasValue(regions[2]) && (
                  <Grid.Column>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('RegionLvl3')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        <label className={styles.labelRegion}>{regions[2]}</label>
                        {regionMenu3.value}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
                {hasValue(regions[3]) && (
                  <Grid.Column>
                    <div className={styles.container}>
                      <label className={styles.boldText}>{localize('RegionLvl4')}</label>
                      <Label className={styles.labelStyle} basic size="large">
                        <label className={styles.labelRegion}>{regions[3]}</label>
                        {regionMenu4.value}
                      </Label>
                    </div>
                  </Grid.Column>
                )}
              </Grid.Row>
            )}
            <Grid.Row>
              <Grid.Column width={16}>
                <label className={styles.boldText}>{localize('PersonsRelatedToTheUnit')}</label>
                <PersonsField name="persons" value={data.persons} localize={localize} readOnly />
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Segment>
      </div>
    )
  }
}

export default ContactInfo
