using NAudio.Wave;

namespace usicMusic.Core
{
    public class LoopbackRecorder
    {
        private static WasapiLoopbackCapture CaptureInstance;
        private static WaveFileWriter RecordedAudioWriter;

        public void StartRecording(string filename)
        {
            CaptureInstance = new WasapiLoopbackCapture();
            RecordedAudioWriter = new WaveFileWriter(filename, CaptureInstance.WaveFormat);
            RecordedAudioWriter.WriteSample(16000);

            CaptureInstance.DataAvailable += (s, a) =>
            {
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            CaptureInstance.RecordingStopped += (s, a) =>
            {
                RecordedAudioWriter.Dispose();
                RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            CaptureInstance.StartRecording();
        }

        public void StopRecording()
        {
            CaptureInstance.StopRecording();
        }
    }
}