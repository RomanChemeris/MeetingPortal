class MeetingRequestsTable extends React.Component {
    constructor(props) {
        super(props);
        this.handleBtnSubmit = this.handleBtnSubmit.bind(this);
    }
    handleBtnSubmit(e) {
        this.props.onBtnSubmit(e);
    }
    render() {
        const requestNodes = this.props.data.map(request => (
            <MeetingRequest data={request} key={request.Id} onBtnSubmit={this.handleBtnSubmit}/>
        ));
        return (
            <table className="table table-light">
                <thead>
                    <tr>
                        <th scope="col">
                            Название заявки
                        </th>
                        <th scope="col">
                            Время бронирования
                        </th>
                        <th scope="col">
                            Название переговорной
                        </th>
                        <th scope="col">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {requestNodes}
                </tbody>
            </table>
        );
    }
}

class MeetingRequest extends React.Component {
    constructor(props) {
        super(props);
        this.handleBtnSubmit = this.handleBtnSubmit.bind(this);
    }
    handleBtnSubmit(e, data) {
        this.props.onBtnSubmit(data);
    }
    render() {
        return (
            <tr>
                <td>{this.props.data.Name}</td>
                <td>{this.props.data.BookingTime}</td>
                <td>{this.props.data.RoomName}</td>
                <td className="btn-group">
                    <button className="btn btn-success" onClick={(e) => this.handleBtnSubmit(e, { id: this.props.data.Id, accept: true })}>Подтвердить</button>
                    <button className="btn btn-danger" onClick={(e) => this.handleBtnSubmit(e, { id: this.props.data.Id, accept: false })}>Отклонить</button>
                </td>
            </tr>
        );
    }
}

class MeetingBoxRequests extends React.Component {
    constructor(props) {
        super(props);
        this.state = { meetingRequests: [] };
        this.handleBtnSubmit = this.handleBtnSubmit.bind(this);
    }
    componentDidMount() {
        this.loadMeetingRequestsFromServer();
        window.setInterval(() => this.loadMeetingRequestsFromServer(), this.props.pollInterval);
    }
    handleBtnSubmit(obj) {
        const data = new FormData();
        data.append('id', obj.id);
        data.append('accept', obj.accept);

        const xhr = new XMLHttpRequest();
        xhr.open('post', this.props.submitUrl, true);
        xhr.onload = () => this.loadMeetingRequestsFromServer();
        xhr.send(data);
    }
    loadMeetingRequestsFromServer() {
        const xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = () => {
            const data = JSON.parse(xhr.responseText);
            this.setState({ meetingRequests: data });
        };
        xhr.send();
    }
    render() {
        return (
            <div className="meeting-box animated fadeIn">
                <div className="jumbotron">
                    <h2>Бронирование переговорных</h2>
                    <MeetingRequestsTable data={this.state.meetingRequests} onBtnSubmit={this.handleBtnSubmit} />
                </div>
            </div>
        );
    }
}

ReactDOM.render(
    <MeetingBoxRequests url="home/RoomRequests" submitUrl="home/ModerateRoomRequest" pollInterval="10000" />, document.getElementById('meetingRoomsRequests')
);