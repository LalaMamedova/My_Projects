import { observer } from 'mobx-react-lite';
import AppRouter from './UserPath/components/AppRouter.jsx'
import {BrowserRouter} from 'react-router-dom'



const App = observer(() => {

  return (
    <BrowserRouter>
        <AppRouter />
    </BrowserRouter>
  );
});

export default App;
