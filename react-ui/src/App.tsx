import * as React from 'react';

import Header from './components/common/header';
import Usage from './components/usage';

class App extends React.Component {

  render() {
    return (
      <div>
        <Header />
        <Usage />
      </div>
    );
  }
}

export default App;
