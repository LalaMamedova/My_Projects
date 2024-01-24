import asyncio
from aiogram import Bot, Dispatcher, types
from aiogram.dispatcher.filters.state import State, StatesGroup
from aiogram.utils import executor

stop = False
pause = False
bot = Bot('6631346671:AAFKV8rTDIj7J4RxrOib-BBlaJ8qTWa3li8')
dp = Dispatcher(bot)

inline = types.InlineKeyboardMarkup()
inline.row(types.InlineKeyboardButton(text="Остановить", callback_data='stopTimer'))
inline.row(types.InlineKeyboardButton(text="Поставить на паузу", callback_data='pauseTimer'),types.InlineKeyboardButton(text="Возобновить таймер", callback_data='startTimer'))

class Form(StatesGroup):
    SetATime = State()

@dp.message_handler(commands=['start','timer','set'])
async def start(message: types.Message):
    markup = types.InlineKeyboardMarkup()
    markup.row(types.InlineKeyboardButton(text='СДВГ(10 минут)', callback_data='adhd'),types.InlineKeyboardButton(text='Обычный(20 минут)', callback_data='avarege'))
    markup.row(types.InlineKeyboardButton(text='Гиперфиксация(35 минут)', callback_data='hyper'),types.InlineKeyboardButton(text='Установить таймер', callback_data='settime'))
    await message.answer('Выберите длительность концентрации',reply_markup=markup)


@dp.callback_query_handler()
async def inline_callback(callback_query: types.CallbackQuery):

    global stop, pause
    actions = {
        'adhd': ('⏲️ Начинаю таймер на 10 минут...', 10 * 60),
        'average': ('⏲️ Начинаю таймер на 20 минут...', 20 * 60),
        'hyper': ('⏲️ Начинаю таймер на 35 минут...', 35 * 60),
    }
    action = actions.get(callback_query.data)

    if action:
        message_text, duration = action
        if duration:
            timer_message = await bot.send_message(callback_query.from_user.id, message_text)
            await update_timer(timer_message, duration)

    elif (callback_query.data == 'settime'):
        await bot.send_message(callback_query.from_user.id, 'Установите таймер')
        await Form.SetATime.set()
    elif(callback_query.data == 'pauseTimer'):
        if(pause == False):
            pause = True
            await callback_query.answer('Таймер на паузе')
    elif (callback_query.data == 'startTimer'):
        if (pause == True):
            await callback_query.answer('Таймер возобновлен')
            pause = False
    else:
        stop = True


@dp.message_handler(state=Form.SetATime)
async def setTime(message: types.Message):
    try:
        if (int(message.text)):
            minut = int(message.text)
            await update_timer(message, minut * 60)
    except:
        await bot.send_message(message.chat.id, 'Ошибка. Введите время в числах!(например 30)')



async def update_timer(message: types.Message, seconds):
    await timerTemplate(message,seconds,'Осталось концентрироваться','⏲️')
    await breakTime(message)



async def breakTime(message: types.Message):
    await bot.send_message(message.chat.id, '⏲️Таймер завершен.\nПора отдыхать!!')
    await timerTemplate(message,300,'Отдыхаем','🧘')
    await bot.send_message(message.chat.id,'Не желаете повторить?')
    await start(message)



async def timerTemplate(message: types.Message, seconds,whatDoing,emoji):
    global stop,pause

    for remaining_seconds in range(seconds, 0, -1):
        minutes, seconds = divmod(remaining_seconds, 60)
        timer_message = f'{emoji} {whatDoing}: <b>{minutes} минут {seconds} секунд</b>'
        await bot.edit_message_text(text=timer_message, chat_id=message.chat.id, message_id=message.message_id,
                                    parse_mode='HTML', reply_markup=inline)
        if (stop == True):
            stop = False
            break

        while (pause == True):
            await asyncio.sleep(0.5)
        await asyncio.sleep(1)


async def sendMinuts(message,minutes,whatDoing,emoji):
    timer_message = f'<b>{emoji} {whatDoing}:</b> <b>{minutes/60} минут {0} секунд</b>'
    await bot.send_message(chat_id=message.chat.id,text=timer_message, parse_mode='HTML')

executor.start_polling(dp,  skip_updates=True)