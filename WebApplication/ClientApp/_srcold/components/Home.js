import React, { Component } from 'react';

export class Home extends Component {

    constructor(props) {
        super(props);
        this.state = {user: '', message:'', messageList:''};
        
        this.handleMessage = this.handleMessage.bind(this);
        this.handleUserName = this.handleUserName.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
  
  render() {
    return (
      <div>
          <form onSubmit={this.handleSubmit}>
              <label>
                  Name:
                  <input type="text" name="username" value={this.state.user} onChange={this.handleUserName}/>
              </label>
              <label htmlFor="message">Message: </label>

              <input type="text" name="message" value={this.state.message} onChange={this.handleMessage}/>

              <input type="submit" value="Submit" />
          </form>
      </div>
    );
  }

    handleUserName(event) {
        this.setState({user: event.target.value});
    }

    handleMessage(event) {
        this.setState({message: event.target.value});
    }

    handleSubmit(event) {
        event.preventDefault();
    }
}
