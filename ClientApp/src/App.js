import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Supermarket } from './components/Supermarket';
import { Checkout } from './components/Checkout';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Supermarket} />
        <Route path='/Supermarket/Checkout' component={Checkout} />
      </Layout>
    );
  }
}
