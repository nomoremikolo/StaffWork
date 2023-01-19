import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface MessageType {
    id: number;
    message: string;
    isActive: boolean | undefined;
    isSuccess: boolean;
    isError: boolean;
    isWarning: boolean;
}

interface NotificationState {
    messages: MessageType[] | null;
}

const initialState: NotificationState = {
    messages: null,
};

export const NotificationReducer = createSlice({
    name: "MessageReducer",
    initialState: initialState,
    reducers: {
        SHOW_SUCCESS_MESSAGE(state, action: PayloadAction<string>) {
            const actualId = state.messages ? state.messages.length : 0;
            if (state.messages) {
                state.messages = [
                    ...state.messages,
                    { id: actualId, message: action.payload, isActive: true, isSuccess: true, isError: false, isWarning: false },
                ];
            } else {
                state.messages = [
                    { id: actualId, message: action.payload, isActive: true, isSuccess: true, isError: false, isWarning: false },
                ];
            }
            console.log("SUCCESS: " + state.messages[actualId].message);
        },
        SHOW_ERROR_MESSAGE(state, action: PayloadAction<string>) {
            const actualId = state.messages ? state.messages.length : 0;
            if (state.messages) {
                state.messages = [
                    ...state.messages,
                    { id: actualId, message: action.payload, isActive: true, isSuccess: false, isError: true, isWarning: false },
                ];
            } else {
                state.messages = [
                    { id: actualId, message: action.payload, isActive: true, isSuccess: false, isError: true, isWarning: false },
                ];
            }
            console.error("ERROR: " + state.messages[actualId].message);
        },
        SHOW_WARNING_MESSAGE(state, action: PayloadAction<string>) {
            const actualId = state.messages ? state.messages.length : 0;
            if (state.messages) {
                state.messages = [
                    ...state.messages,
                    { id: actualId, message: action.payload, isActive: true, isSuccess: false, isError: false, isWarning: true },
                ];
            } else {
                state.messages = [
                    { id: actualId, message: action.payload, isActive: true, isSuccess: false, isError: false, isWarning: true },
                ];
            }
            console.warn("WARNING: " + state.messages[actualId].message);
        },
        DEACTIVATE_MESSAGE(state, action: PayloadAction<number>) {
            if (state.messages) {
                state.messages = state.messages.map((message) => {
                    if (message.id === action.payload) {
                        message.isActive = false;
                    }
                    return message;
                });
            }
        },
    },
});

export default NotificationReducer.reducer;