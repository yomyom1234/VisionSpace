import cv2
import mediapipe as mp
import socket

UDP_IP = "127.0.0.1"
UDP_PORT = 21900
# UDP
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
hands = mp.solutions.hands.Hands(static_image_mode=False,
                                 max_num_hands=1,
                                 model_complexity=0,
                                 min_detection_confidence=0.5,
                                 min_tracking_confidence=0.5)
cap = cv2.VideoCapture(0)
if cap.isOpened():
    width = cap.get(cv2.CAP_PROP_FRAME_WIDTH)
    height = cap.get(cv2.CAP_PROP_FRAME_HEIGHT)
    camFps = cap.get(cv2.CAP_PROP_FPS)
    print('Cam working normally, ' + str(width) + ' ' + str(height) + ' '
          + str(camFps) + '. Press ESC to end.')
else:
    print('Cam error')
while cap.isOpened():
    camWorked, image = cap.read()
    if not camWorked:
        print("Ignoring empty camera frame.")
        continue
    image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
    image.flags.writeable = False
    mp_outputs = hands.process(image)
    mp_landmarks = mp_outputs.multi_hand_landmarks
    index = 0
    if mp_landmarks is not None:
        socket_msg = ''
        for landmarks in mp_landmarks:
            handedness = mp_outputs.multi_handedness[index].classification[0].label
            socket_msg += handedness + ','
            for landmark in landmarks.landmark:
                socket_msg += str(round(landmark.x, 2)) + ',' + str(round(landmark.y, 2)) + ',' \
                              + str(round(landmark.z, 2)) + ','  # + str(landmark.visibility) + ','
            index += 1
        sand_socket_msg = bytearray(str(index) + ',' + socket_msg, 'utf-8')
        sock.sendto(sand_socket_msg, (UDP_IP, UDP_PORT))
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    if mp_outputs.multi_hand_landmarks:
        for hand_landmarks in mp_outputs.multi_hand_landmarks:
            mp.solutions.drawing_utils.draw_landmarks(
                image,
                hand_landmarks,
                mp.solutions.hands.HAND_CONNECTIONS,
                mp.solutions.drawing_styles.get_default_hand_landmarks_style(),
                mp.solutions.drawing_styles.get_default_hand_connections_style())
    cv2.imshow('MediaPipe Hands', image)
    if cv2.waitKey(5) & 0xFF == 27:  # esc（27 )
        break

hands.close()
cap.release()
